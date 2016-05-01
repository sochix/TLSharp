﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TLSharp.Core.Auth;
using TLSharp.Core.MTProto;
using TLSharp.Core.MTProto.Crypto;
using TLSharp.Core.Network;
using TLSharp.Core.Requests;
using TLSharp.Core.Utils;
using MD5 = System.Security.Cryptography.MD5;

namespace TLSharp.Core
{
    public class TelegramClient
    {
        private MtProtoSender _sender;
        private TcpTransport _transport;
        private string _apiHash = "";
        private int _apiId = 0;
        private Session _session;
        private List<DcOption> dcOptions;

        public TelegramClient(ISessionStore store, string sessionUserId, int apiId, string apiHash) : this(store, sessionUserId, apiId, apiHash, null, 0)
        {
        }

        public TelegramClient(ISessionStore store, string sessionUserId, int apiId, string apiHash, string dcAddress, int dcPort)
        {
            _apiHash = apiHash;
            _apiId = apiId;
            if (_apiId == 0)
                throw new InvalidOperationException("Your API_ID is invalid. Do a configuration first https://github.com/sochix/TLSharp#quick-configuration");

            if (string.IsNullOrEmpty(_apiHash))
                throw new InvalidOperationException("Your API_ID is invalid. Do a configuration first https://github.com/sochix/TLSharp#quick-configuration");

            if (string.IsNullOrEmpty(dcAddress))
            {
                _session = Session.TryLoadOrCreateNew(store, sessionUserId);
            }
            else
            {
                _session = Session.TryLoadOrCreateNew(store, sessionUserId, dcAddress, dcPort);
            }
        }

        public void SetFloodWaitEvent(MtProtoSender.FloodWait floodWaitEvent)
        {
            if (_sender == null)
            {
                throw new InvalidOperationException("Telegram client cannot connected now.");
            }
            _sender.FloodWaitEvent = floodWaitEvent;
        }

        public async Task<bool> Connect(bool reconnect = false)
        {
            if (_transport == null)
            {
                _transport = new TcpTransport(_session.ServerAddress, _session.Port);
            }

            if (_session.AuthKey == null || reconnect)
            {
                var result = await Authenticator.DoAuthentication(_transport);
                _session.AuthKey = result.AuthKey;
                _session.TimeOffset = result.TimeOffset;
            }

            _sender = new MtProtoSender(_transport, _session);

            if (!reconnect)
            {
                var request = new InitConnectionRequest(_apiId);

                await _sender.Send(request);
                await _sender.Recieve(request);

                dcOptions = request.ConfigConstructor.dc_options;
                _session.CurrentDcId = request.ConfigConstructor.this_dc;
            }

            return true;
        }

        private async Task ReconnectToDc(int dcId)
        {
            if (dcOptions == null || !dcOptions.Any())
                throw new InvalidOperationException($"Can't reconnect. Establish initial connection first.");

            var dc = dcOptions.Cast<DcOptionConstructor>().First(d => d.id == dcId);

            if (_transport != null)
            {
                _transport.Dispose();
            }
            _transport = new TcpTransport(dc.ip_address, dc.port);
            _session.ServerAddress = dc.ip_address;
            _session.Port = dc.port;
            _session.CurrentDcId = dcId;

            await Connect(true);
        }

        public bool IsUserAuthorized()
        {
            return _session.User != null && !IsSessionExpired();
        }

        public bool IsSessionExpired()
        {
            return GetSessionExpireTime().CompareTo(DateTime.Now) <= 0;
        }

        public DateTime GetSessionExpireTime()
        {
            return Helpers.UnixToDateTime(_session.SessionExpires);
        }

        public async Task<bool> IsPhoneRegistered(string phoneNumber)
        {
            if (_sender == null)
                throw new InvalidOperationException("Not connected!");

            bool completed = false;
            AuthCheckPhoneRequest authCheckPhoneRequest = null;

            while (!completed)
            {
                authCheckPhoneRequest = new AuthCheckPhoneRequest(phoneNumber);

                try
                {
                    await _sender.Send(authCheckPhoneRequest);
                    await _sender.Recieve(authCheckPhoneRequest);
                    completed = true;
                }
                catch (InvalidOperationException e)
                {
                    if (e.Message.StartsWith("Your phone number registered to") && e.Data["dcId"] != null)
                    {
                        await ReconnectToDc((int)e.Data["dcId"]);
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return authCheckPhoneRequest._phoneRegistered;
        }

        public async Task<string> SendCodeRequest(string phoneNumber, int requestType = 5)
        {
            var completed = false;

            AuthSendCodeRequest request = null;

            while (!completed)
            {
                request = new AuthSendCodeRequest(phoneNumber, requestType, _apiId, _apiHash, "en");
                try
                {
                    await _sender.Send(request);
                    await _sender.Recieve(request);

                    completed = true;
                }
                catch (InvalidOperationException ex)
                {
                    if (ex.Message.StartsWith("Your phone number registered to") && ex.Data["dcId"] != null)
                    {
                        await ReconnectToDc((int)ex.Data["dcId"]);
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return request._phoneCodeHash;
        }

        public async Task<User> MakeAuth(string phoneNumber, string phoneHash, string code)
        {
            var request = new AuthSignInRequest(phoneNumber, phoneHash, code);
            await _sender.Send(request);
            await _sender.Recieve(request);

            _session.SessionExpires = request.SessionExpires;
            _session.User = request.user;

            _session.Save();

            return request.user;
        }

        public async Task<InputFile> UploadFile(string name, byte[] data)
        {
            var partSize = 65536;

            var file_id = DateTime.Now.Ticks;

            var partedData = new Dictionary<int, byte[]>();
            var parts = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(data.Length) / Convert.ToDouble(partSize)));
            var remainBytes = data.Length;
            for (int i = 0; i < parts; i++)
            {
                partedData.Add(i, data
                    .Skip(i * partSize)
                    .Take(remainBytes < partSize ? remainBytes : partSize)
                    .ToArray());

                remainBytes -= partSize;
            }

            for (int i = 0; i < parts; i++)
            {
                var saveFilePartRequest = new Upload_SaveFilePartRequest(file_id, i, partedData[i]);
                await _sender.Send(saveFilePartRequest);
                await _sender.Recieve(saveFilePartRequest);

                if (saveFilePartRequest.Done == false)
                    throw new InvalidOperationException($"File part {i} does not uploaded");
            }

            string md5_checksum;
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(data);
                var hashResult = new StringBuilder(hash.Length * 2);

                for (int i = 0; i < hash.Length; i++)
                    hashResult.Append(hash[i].ToString("x2"));

                md5_checksum = hashResult.ToString();
            }

            var inputFile = new InputFileConstructor(file_id, parts, name, md5_checksum);

            return inputFile;
        }

        public async Task<bool> SendMediaMessage(int contactId, InputFile file)
        {
            var request = new Message_SendMediaRequest(
                new InputPeerContactConstructor(contactId),
                new InputMediaUploadedPhotoConstructor(file));

            await _sender.Send(request);
            await _sender.Recieve(request);

            return true;
        }

        public async Task<int?> ImportContactByPhoneNumber(string phoneNumber)
        {
            if (!validateNumber(phoneNumber))
                throw new InvalidOperationException("Invalid phone number. It should be only digit string, from 5 to 20 digits.");

            var request = new ImportContactRequest(new InputPhoneContactConstructor(0, phoneNumber, "My Test Name", String.Empty));
            await _sender.Send(request);
            await _sender.Recieve(request);

            var importedUser = (ImportedContactConstructor)request.imported.FirstOrDefault();

            return importedUser?.user_id;
        }

        public async Task<int?> ImportByUserName(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new InvalidOperationException("Username can't be null");

            var request = new ImportByUserName(username);
            await _sender.Send(request);
            await _sender.Recieve(request);

            return request.id;
        }

        public async Task SendMessage(int id, string message)
        {
            var request = new SendMessageRequest(new InputPeerContactConstructor(id), message);

            await _sender.Send(request);
            await _sender.Recieve(request);
        }

        public async Task<List<Message>> GetMessagesHistoryForContact(int user_id, int offset, int limit, int max_id = -1)
        {
            var request = new GetHistoryRequest(new InputPeerContactConstructor(user_id), offset, max_id, limit);
            await _sender.Send(request);
            await _sender.Recieve(request);

            return request.messages;
        }

        public async Task<contacts_Contacts> GetContacts()
        {
            var req = new GetContacts();
            await _sender.Send(req);
            await _sender.Recieve(req);

            return req.contacts;
        }

        //Limit req: value % 1024
        public async Task<Upload_fileConstructor> GetFile(InputFileLocation ifl, int size, int limit = 1024)
        {
            var result = new Upload_fileConstructor();
            result.bytes = new byte[size];
            GetFileRequest req;
            for (int recieved = 0; recieved < size; recieved += req.bytes.Length)
            {
                req = new GetFileRequest(ifl, recieved, limit);
                await _sender.Send(req);
                await _sender.Recieve(req);

                result.type = req.type;
                result.mtime = req.mtime;

                req.bytes.CopyTo(result.bytes, recieved);
            }
            return result;
        }

        private bool validateNumber(string number)
        {
            var regex = new Regex("^\\d{7,20}$");

            return regex.IsMatch(number);
        }

        public void Dispose()
        {
            if (_transport != null)
            {
                _transport.Dispose();
                _transport = null;
            }

            _sender = null;
            dcOptions = null;
        }
    }
}
