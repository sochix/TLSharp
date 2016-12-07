﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TeleSharp.TL;
using TeleSharp.TL.Auth;
using TeleSharp.TL.Contacts;
using TeleSharp.TL.Help;
using TeleSharp.TL.Messages;
using TeleSharp.TL.Upload;
using TLSharp.Core.Auth;
using TLSharp.Core.MTProto.Crypto;
using TLSharp.Core.Network;
using TLSharp.Core.Requests;
using TLSharp.Core.Utils;

namespace TLSharp.Core
{
    public class TelegramClient
    {
        private MtProtoSender _sender;
        private AuthKey _key;
        private TcpTransport _transport;
        private string _apiHash = "";
        private int _apiId = 0;
        private Session _session;
        private List<TLDcOption> dcOptions;

        public TelegramClient(int apiId, string apiHash, ISessionStore store = null, string sessionUserId = "session")
        {
            if (store == null)
                store = new FileSessionStore();

            TLContext.Init();
            _apiHash = apiHash;
            _apiId = apiId;
            if (_apiId == default(int))
                throw new MissingApiConfigurationException("API_ID");
            if (string.IsNullOrEmpty(_apiHash))
                throw new MissingApiConfigurationException("API_HASH");

            _session = Session.TryLoadOrCreateNew(store, sessionUserId);
            _transport = new TcpTransport(_session.ServerAddress, _session.Port);
        }

        public TelegramClient(int apiId, string apiHash, string httpProxyHost, int httpProxyPort, string proxyUserName, string proxyPassword,
            ISessionStore store = null, string sessionUserId = "session")
        {
            if (store == null)
                store = new FileSessionStore();

            TLContext.Init();
            _apiHash = apiHash;
            _apiId = apiId;
            if (_apiId == default(int))
                throw new MissingApiConfigurationException("API_ID");
            if (string.IsNullOrEmpty(_apiHash))
                throw new MissingApiConfigurationException("API_HASH");

            _session = Session.TryLoadOrCreateNew(store, sessionUserId);
            _transport = new TcpTransport(_session.ServerAddress, _session.Port,
                httpProxyHost, httpProxyPort, proxyUserName, proxyPassword);

        }


        public async Task<bool> ConnectAsync(bool reconnect = false)
        {
            if (_session.AuthKey == null || reconnect)
            {
                var result = await Authenticator.DoAuthentication(_transport);
                _session.AuthKey = result.AuthKey;
                _session.TimeOffset = result.TimeOffset;
            }

            _sender = new MtProtoSender(_transport, _session);

            //set-up layer
            var config = new TLRequestGetConfig();
            var request = new TLRequestInitConnection()
            {
                api_id = _apiId,
                app_version = "1.0.0",
                device_model = "PC",
                lang_code = "en",
                query = config,
                system_version = "Win 10.0"
            };
            var invokewithLayer = new TLRequestInvokeWithLayer() { layer = 57, query = request };
            await _sender.Send(invokewithLayer);
            await _sender.Receive(invokewithLayer);

            dcOptions = ((TLConfig)invokewithLayer.Response).dc_options.lists;

            return true;
        }

        private async Task ReconnectToDcAsync(int dcId)
        {
            if (dcOptions == null || !dcOptions.Any())
                throw new InvalidOperationException($"Can't reconnect. Establish initial connection first.");

            var dc = dcOptions.First(d => d.id == dcId);

            _transport = new TcpTransport(dc.ip_address, dc.port);
            _session.ServerAddress = dc.ip_address;
            _session.Port = dc.port;

            await ConnectAsync(true);
        }

        public bool IsUserAuthorized()
        {
            return _session.TLUser != null;
        }

        public async Task<bool> IsPhoneRegisteredAsync(string phoneNumber)
        {
            if (_sender == null)
                throw new InvalidOperationException("Not connected!");

            var authCheckPhoneRequest = new TLRequestCheckPhone() { phone_number = phoneNumber };
            await _sender.Send(authCheckPhoneRequest);
            await _sender.Receive(authCheckPhoneRequest);

            return authCheckPhoneRequest.Response.phone_registered;
        }

        public async Task<string> SendCodeRequestAsync(string phoneNumber)
        {
            var completed = false;

            TLRequestSendCode request = null;

            while (!completed)
            {
                request = new TLRequestSendCode() { phone_number = phoneNumber, api_id = _apiId, api_hash = _apiHash };
                try
                {
                    await _sender.Send(request);
                    await _sender.Receive(request);

                    completed = true;
                }
                catch (MigrationNeededException ex)
                {
                    await ReconnectToDcAsync(ex.DC);
                }
            }

            return request.Response.phone_code_hash;
        }

        public async Task<TLUser> MakeAuthAsync(string phoneNumber, string phoneCodeHash, string code)
        {
            var request = new TLRequestSignIn() { phone_number = phoneNumber, phone_code_hash = phoneCodeHash, phone_code = code };
            await _sender.Send(request);
            await _sender.Receive(request);

            OnUserAuthenticated(((TLUser)request.Response.user));

            return ((TLUser)request.Response.user);
        }

        public async Task<TLUser> SignUpAsync(string phoneNumber, string phoneCodeHash, string code, string firstName, string lastName)
        {
            var request = new TLRequestSignUp() { phone_number = phoneNumber, phone_code = code, phone_code_hash = phoneCodeHash, first_name = firstName, last_name = lastName };
            await _sender.Send(request);
            await _sender.Receive(request);

            OnUserAuthenticated(((TLUser)request.Response.user));

            return ((TLUser)request.Response.user);
        }
        public async Task<T> SendRequestAsync<T>(TLMethod methodtoExceute)
        {
            await _sender.Send(methodtoExceute);
            await _sender.Receive(methodtoExceute);

            var result = methodtoExceute.GetType().GetProperty("Response").GetValue(methodtoExceute);

            return (T)result;
        }

        public async Task<TLContacts> GetContactsAsync()
        {
            if (!IsUserAuthorized())
                throw new InvalidOperationException("Authorize user first!");

            var req = new TLRequestGetContacts() { hash = "" };

            return await SendRequestAsync<TLContacts>(req);
        }

        public async Task<TLAbsUpdates> SendMessageAsync(TLAbsInputPeer peer, string message)
        {
            if (!IsUserAuthorized())
                throw new InvalidOperationException("Authorize user first!");

            return await SendRequestAsync<TLAbsUpdates>(
                   new TLRequestSendMessage()
                   {
                       peer = peer,
                       message = message,
                       random_id = Helpers.GenerateRandomLong()
                   });
        }

        public async Task<Boolean> SendTypingAsync(TLAbsInputPeer peer)
        {
            var req = new TLRequestSetTyping()
            {
                action = new TLSendMessageTypingAction(),
                peer = peer
            };
            return await SendRequestAsync<Boolean>(req);
        }

        public async Task<TLDialogs> GetUserDialogsAsync()
        {
            var peer = new TLInputPeerSelf();
            return await SendRequestAsync<TLDialogs>(
                new TLRequestGetDialogs() { offset_date = 0, offset_peer = peer, limit = 100 });
        }

        public async Task<TLAbsUpdates> SendUploadedPhoto(TLAbsInputPeer peer, TLAbsInputFile file, string caption)
        {   
            return await SendRequestAsync<TLAbsUpdates>(new TLRequestSendMedia()
            {
                random_id = Helpers.GenerateRandomLong(),
                background = false,
                clear_draft = false,
                media = new TLInputMediaUploadedPhoto() { file = file, caption = caption },
                peer = peer
            });
        }

        public async Task<TLAbsUpdates> SendUploadedDocument(
            TLAbsInputPeer peer, TLAbsInputFile file, string caption, string mimeType, TLVector<TLAbsDocumentAttribute> attributes)
        {
           return await SendRequestAsync<TLAbsUpdates>(new TLRequestSendMedia()
            {
                random_id = Helpers.GenerateRandomLong(),
                background = false,
                clear_draft = false,
                media = new TLInputMediaUploadedDocument()
                {
                    file = file,
                    caption = caption,
                    mime_type = mimeType,
                    attributes = attributes
                },
                peer = peer
            });
        }

        public async Task<TLFile> GetFile(TLAbsInputFileLocation location, int filePartSize)
        {
            return await SendRequestAsync<TLFile>(new TLRequestGetFile()
            {
                location = location,
                limit = filePartSize
            });
        } 

        private void OnUserAuthenticated(TLUser TLUser)
        {
            _session.TLUser = TLUser;
            _session.SessionExpires = int.MaxValue;

            _session.Save();
        }
    }

    public class MissingApiConfigurationException : Exception
    {
        public const string InfoUrl = "https://github.com/sochix/TLSharp#quick-configuration";

        internal MissingApiConfigurationException(string invalidParamName):
            base($"Your {invalidParamName} setting is missing. Adjust the configuration first, see {InfoUrl}")
        {
        }
    }
}
