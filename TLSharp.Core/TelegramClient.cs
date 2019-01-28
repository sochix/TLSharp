﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
using TeleSharp.TL.Account;
using TeleSharp.TL.Auth;
using TeleSharp.TL.Contacts;
using TeleSharp.TL.Help;
using TeleSharp.TL.Messages;
using TeleSharp.TL.Upload;
using TLSharp.Core.Auth;
using TLSharp.Core.MTProto.Crypto;
using TLSharp.Core.Network;
using TLSharp.Core.Utils;
using TLAuthorization = TeleSharp.TL.Auth.TLAuthorization;

namespace TLSharp.Core
{
    public class TelegramClient : IDisposable
    {
        private MtProtoSender _sender;
        private readonly AuthKey _key;
        private TcpTransport _transport;
        private string _apiHash = "";
        private int _apiId = 0;
        private Session _session;
        private List<TLDcOption> dcOptions;
        private readonly TcpClientConnectionHandler _handler;

        public TelegramClient(int apiId, string apiHash,
            ISessionStore store = null, string sessionUserId = "session", TcpClientConnectionHandler handler = null)
        {
            if (apiId == default(int))
                throw new MissingApiConfigurationException("API_ID");
            if (string.IsNullOrEmpty(apiHash))
                throw new MissingApiConfigurationException("API_HASH");

            if (store == null)
                store = new FileSessionStore();

            _apiHash = apiHash;
            _apiId = apiId;
            _handler = handler;

            _session = Session.TryLoadOrCreateNew(store, sessionUserId);
            _transport = new TcpTransport(_session.ServerAddress, _session.Port, _handler);
        }

        public void ConnectAsync(bool reconnect = false)
        {
            if (_session.AuthKey == null || reconnect)
            {
                var result = Authenticator.DoAuthentication(_transport);
                _session.AuthKey = result.AuthKey;
                _session.TimeOffset = result.TimeOffset;
            }

            _sender = new MtProtoSender(_transport, _session);

            //set-up layer
            var config = new TLRequestGetConfig();
            var request = new TLRequestInitConnection()
            {
                ApiId = _apiId,
                AppVersion = "1.0.0",
                DeviceModel = "PC",
                LangCode = "en",
                Query = config,
                SystemVersion = "Win 10.0"
            };
            var invokewithLayer = new TLRequestInvokeWithLayer() { Layer = 66, Query = request };
            _sender.Send(invokewithLayer);
            _sender.Receive(invokewithLayer);

            dcOptions = ((TLConfig)invokewithLayer.Response).DcOptions.ToList();
        }

        private void ReconnectToDcAsync(int dcId, int times)
        {
            if (dcOptions == null || !dcOptions.Any())
                throw new InvalidOperationException($"Can't reconnect. Establish initial connection first.");

            TLDcOption dc = null;
            foreach (var d2 in dcOptions)
            {
                if (d2.Id == dcId && d2.Ipv6 == false)
                {
                    dc = d2;
                    break;
                }
            }

            
            TLExportedAuthorization exported = null;
            if (_session.TLUser != null)
            {
                TLRequestExportAuthorization exportAuthorization = new TLRequestExportAuthorization() { DcId = dcId };
                exported = SendRequestAsync<TLExportedAuthorization>(exportAuthorization, times);
            }
            

            _transport = new TcpTransport(dc.IpAddress, dc.Port, _handler);
            _session.ServerAddress = dc.IpAddress;
            _session.Port = dc.Port;

            ConnectAsync(true);

            
            if (_session.TLUser != null)
            {
                TLRequestImportAuthorization importAuthorization = new TLRequestImportAuthorization() { Id = exported.Id, Bytes = exported.Bytes };
                var imported = SendRequestAsync<TLAuthorization>(importAuthorization,times);
                OnUserAuthenticated(((TLUser)imported.User));
            }
            

        }

        private void RequestWithDcMigration(TLMethod request, int times)
        {
            if (_sender == null)
                throw new InvalidOperationException("Not connected!");

            var completed = false;
            while(!completed)
            {
                try
                {
                    _sender.Send(request);
                    _sender.Receive(request);
                    completed = true;
                }
                catch(DataCenterMigrationException e)
                {
                    if (times <= 150)
                    {
                        ReconnectToDcAsync(e.DC, times + 1);
                        // prepare the request for another try
                        request.ConfirmReceived = false;
                    }
                    else
                    {
                        throw e;
                    }
                }
                catch (TLSharp.Core.Network.FloodException e)
                {
                    throw e;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public bool IsUserAuthorized()
        {
            return _session.TLUser != null;
        }

        public bool IsPhoneRegisteredAsync(string phoneNumber)
        {
            if (String.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentNullException(nameof(phoneNumber));

            var authCheckPhoneRequest = new TLRequestCheckPhone() { PhoneNumber = phoneNumber };

            RequestWithDcMigration(authCheckPhoneRequest,0);

            return authCheckPhoneRequest.Response.PhoneRegistered;
        }

        public string SendCodeRequestAsync(string phoneNumber)
        {
            if (String.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentNullException(nameof(phoneNumber));

            var request = new TLRequestSendCode() { PhoneNumber = phoneNumber, ApiId = _apiId, ApiHash = _apiHash };

            RequestWithDcMigration(request,0);

            return request.Response.PhoneCodeHash;
        }

        public TLUser MakeAuthAsync(string phoneNumber, string phoneCodeHash, string code)
        {
            if (String.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentNullException(nameof(phoneNumber));

            if (String.IsNullOrWhiteSpace(phoneCodeHash))
                throw new ArgumentNullException(nameof(phoneCodeHash));

            if (String.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException(nameof(code));
            
            var request = new TLRequestSignIn() { PhoneNumber = phoneNumber, PhoneCodeHash = phoneCodeHash, PhoneCode = code };

            RequestWithDcMigration(request,0);

            OnUserAuthenticated(((TLUser)request.Response.User));

            return ((TLUser)request.Response.User);
        }
        
        public TLPassword GetPasswordSetting()
        {
            var request = new TLRequestGetPassword();

            RequestWithDcMigration(request,0);

            return ((TLPassword)request.Response);
        }

        public TLUser MakeAuthWithPasswordAsync(TLPassword password, string password_str)
        {

            byte[] password_Bytes = Encoding.UTF8.GetBytes(password_str);
            IEnumerable<byte> rv = password.CurrentSalt.Concat(password_Bytes).Concat(password.CurrentSalt);

            SHA256Managed hashstring = new SHA256Managed();
            var password_hash = hashstring.ComputeHash(rv.ToArray());

            var request = new TLRequestCheckPassword() { PasswordHash = password_hash };

            RequestWithDcMigration(request,0);

            OnUserAuthenticated(((TLUser)request.Response.User));

            return ((TLUser)request.Response.User);
        }

        public TLUser SignUpAsync(string phoneNumber, string phoneCodeHash, string code, string firstName, string lastName)
        {
            var request = new TLRequestSignUp() { PhoneNumber = phoneNumber, PhoneCode = code, PhoneCodeHash = phoneCodeHash, FirstName = firstName, LastName = lastName };
            
            RequestWithDcMigration(request,0);

            OnUserAuthenticated(((TLUser)request.Response.User));

            return ((TLUser)request.Response.User);
        }
        public T SendRequestAsync<T>(TLMethod methodToExecute, int times)
        {
            RequestWithDcMigration(methodToExecute, times);

            var result = methodToExecute.GetType().GetProperty("Response").GetValue(methodToExecute);

            return (T)result;
        }

        public TLContacts GetContactsAsync()
        {
            if (!IsUserAuthorized())
                throw new InvalidOperationException("Authorize user first!");

            var req = new TLRequestGetContacts() { Hash = "" };

            return SendRequestAsync<TLContacts>(req,0);
        }

        public TLAbsUpdates SendMessageAsync(TLAbsInputPeer peer, string message)
        {
            if (!IsUserAuthorized())
                throw new InvalidOperationException("Authorize user first!");

            var x = new TLRequestSendMessage()
            {
                Peer = peer,
                Message = message,
                RandomId = Helpers.GenerateRandomLong()
            };
            return SendRequestAsync<TLAbsUpdates>(x, 0);
        }

        public Boolean SendTypingAsync(TLAbsInputPeer peer)
        {
            var req = new TLRequestSetTyping()
            {
                Action = new TLSendMessageTypingAction(),
                Peer = peer
            };
            return SendRequestAsync<Boolean>(req,0);
        }

        public TLAbsDialogs GetUserDialogsAsync(int offsetDate = 0, int offsetId = 0, TLAbsInputPeer offsetPeer = null, int limit = 100)
        {
            if (!IsUserAuthorized())
                throw new InvalidOperationException("Authorize user first!");

            if (offsetPeer == null)
                offsetPeer = new TLInputPeerSelf();

            var req = new TLRequestGetDialogs()
            { 
                OffsetDate = offsetDate, 
                OffsetId = offsetId, 
                OffsetPeer = offsetPeer, 
                Limit = limit
            };
            return SendRequestAsync<TLAbsDialogs>(req,0);
        }

        public TLAbsUpdates SendUploadedPhoto(TLAbsInputPeer peer, TLAbsInputFile file, string caption)
        {
            return SendRequestAsync<TLAbsUpdates>(new TLRequestSendMedia()
            {
                RandomId = Helpers.GenerateRandomLong(),
                Background = false,
                ClearDraft = false,
                Media = new TLInputMediaUploadedPhoto() { File = file, Caption = caption },
                Peer = peer
            },0);
        }

        public TLAbsUpdates SendUploadedDocument(
            TLAbsInputPeer peer, TLAbsInputFile file, string caption, string mimeType, TLVector<TLAbsDocumentAttribute> attributes)
        {
            return SendRequestAsync<TLAbsUpdates>(new TLRequestSendMedia()
            {
                RandomId = Helpers.GenerateRandomLong(),
                Background = false,
                ClearDraft = false,
                Media = new TLInputMediaUploadedDocument()
                {
                    File = file,
                    Caption = caption,
                    MimeType = mimeType,
                    Attributes = attributes
                },
                Peer = peer
            },0);
        }

        public TLFile GetFile(TLAbsInputFileLocation location, int filePartSize, int offset = 0)
        {
            TLFile result = null;
            result = SendRequestAsync<TLFile>(new TLRequestGetFile()
            {
                Location = location,
                Limit = filePartSize,
                Offset = offset
            },0);
            return result;
        }

        public void SendPingAsync()
        {
            _sender.SendPingAsync();
        }

        public TLAbsMessages GetHistoryAsync(TLAbsInputPeer peer, int offsetId = 0, int offsetDate = 0, int addOffset = 0, int limit = 100, int maxId = 0, int minId = 0)
        {
            if (!IsUserAuthorized())
                throw new InvalidOperationException("Authorize user first!");

            var req = new TLRequestGetHistory()
            {
                Peer = peer,
                OffsetId = offsetId,
                OffsetDate = offsetDate,
                AddOffset = addOffset,
                Limit = limit,
                MaxId = maxId,
                MinId = minId
            };
            return SendRequestAsync<TLAbsMessages>(req,0);
        }

        /// <summary>
        /// Serch user or chat. API: contacts.search#11f812d8 q:string limit:int = contacts.Found;
        /// </summary>
        /// <param name="q">User or chat name</param>
        /// <param name="limit">Max result count</param>
        /// <returns></returns>
        public TLFound SearchUserAsync(string q, int limit = 10)
        {
            var r = new TeleSharp.TL.Contacts.TLRequestSearch
            {
                Q = q,
                Limit = limit
            };

            return SendRequestAsync<TLFound>(r,0);
        }

        private void OnUserAuthenticated(TLUser TLUser)
        {
            _session.TLUser = TLUser;
            _session.SessionExpires = int.MaxValue;

            _session.Save();
        }

        public bool IsConnected
        {
            get
            {
                if (_transport == null)
                    return false;
                return _transport.IsConnected;
            }
        }

        public void Dispose()
        {
            if (_transport != null)
            {
                _transport.Dispose();
                _transport = null;
            }
        }
    }

    public class MissingApiConfigurationException : Exception
    {
        public const string InfoUrl = "https://github.com/sochix/TLSharp#quick-configuration";

        internal MissingApiConfigurationException(string invalidParamName) :
            base($"Your {invalidParamName} setting is missing. Adjust the configuration first, see {InfoUrl}")
        {
        }
    }

    public class InvalidPhoneCodeException : Exception
    {
        internal InvalidPhoneCodeException(string msg) : base(msg) { }
    }
    public class CloudPasswordNeededException : Exception
    {
        internal CloudPasswordNeededException(string msg) : base(msg) { }
    }
}
