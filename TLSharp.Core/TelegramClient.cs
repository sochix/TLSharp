using System;
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
        private AuthKey _key;
        private TcpTransport _transport;
        private string _apiHash = "";
        private int _apiId = 0;
        private Session _session;
        private List<TLDcOption> dcOptions;
        private TcpClientConnectionHandler _handler;

        public TelegramClient(int apiId, string apiHash,
            ISessionStore store = null, string sessionUserId = "session", TcpClientConnectionHandler handler = null)
        {
            if (apiId == default(int))
                throw new MissingApiConfigurationException("API_ID");
            if (string.IsNullOrEmpty(apiHash))
                throw new MissingApiConfigurationException("API_HASH");

            if (store == null)
                store = new FileSessionStore();

            TLContext.Init();
            _apiHash = apiHash;
            _apiId = apiId;
            _handler = handler;

            _session = Session.TryLoadOrCreateNew(store, sessionUserId);
            _transport = new TcpTransport(_session.ServerAddress, _session.Port, _handler);
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
            var invokewithLayer = new TLRequestInvokeWithLayer() { layer = 66, query = request };
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

            _transport = new TcpTransport(dc.ip_address, dc.port, _handler);
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
            if (String.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentNullException(nameof(phoneNumber));

            if (_sender == null)
                throw new InvalidOperationException("Not connected!");

            var authCheckPhoneRequest = new TLRequestCheckPhone() { phone_number = phoneNumber };
            var completed = false;
            while(!completed)
            {
                try
                {
                    await _sender.Send(authCheckPhoneRequest);
                    await _sender.Receive(authCheckPhoneRequest);
                    completed = true;
                }
                catch(PhoneMigrationException e)
                {
                    await ReconnectToDcAsync(e.DC);
                }
            }
            return authCheckPhoneRequest.Response.phone_registered;
        }

        public async Task<string> SendCodeRequestAsync(string phoneNumber)
        {
            if (String.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentNullException(nameof(phoneNumber));

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
                catch (PhoneMigrationException ex)
                {
                    await ReconnectToDcAsync(ex.DC);
                }
            }

            return request.Response.phone_code_hash;
        }

        public async Task<TLUser> MakeAuthAsync(string phoneNumber, string phoneCodeHash, string code)
        {
            if (String.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentNullException(nameof(phoneNumber));

            if (String.IsNullOrWhiteSpace(phoneCodeHash))
                throw new ArgumentNullException(nameof(phoneCodeHash));

            if (String.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException(nameof(code));

            var request = new TLRequestSignIn() { phone_number = phoneNumber, phone_code_hash = phoneCodeHash, phone_code = code };

            var completed = false;

            while (!completed)
            {
                try
                {
                    await _sender.Send(request);
                    await _sender.Receive(request);
                    completed = true;
                }
                catch (PhoneMigrationException e)
                {
                    await ReconnectToDcAsync(e.DC);
                }
            }

            OnUserAuthenticated(((TLUser)request.Response.user));

            return ((TLUser)request.Response.user);
        }
        public async Task<TLPassword> GetPasswordSetting()
        {
            var request = new TLRequestGetPassword();

            await _sender.Send(request);
            await _sender.Receive(request);

            return ((TLPassword)request.Response);
        }

        public async Task<TLUser> MakeAuthWithPasswordAsync(TLPassword password, string password_str)
        {

            byte[] password_bytes = Encoding.UTF8.GetBytes(password_str);
            IEnumerable<byte> rv = password.current_salt.Concat(password_bytes).Concat(password.current_salt);

            SHA256Managed hashstring = new SHA256Managed();
            var password_hash = hashstring.ComputeHash(rv.ToArray());

            var request = new TLRequestCheckPassword() { password_hash = password_hash };
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
        public async Task<T> SendRequestAsync<T>(TLMethod methodToExecute)
        {
            await _sender.Send(methodToExecute);
            await _sender.Receive(methodToExecute);

            var result = methodToExecute.GetType().GetProperty("Response").GetValue(methodToExecute);

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

        public async Task<TLAbsDialogs> GetUserDialogsAsync()
        {
            var peer = new TLInputPeerSelf();
            return await SendRequestAsync<TLAbsDialogs>(
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

        public async Task<TLFile> GetFile(TLAbsInputFileLocation location, int filePartSize, int offset = 0)
        {
            TLFile result = null;
            try
            {
                result = await SendRequestAsync<TLFile>(new TLRequestGetFile()
                {
                    location = location,
                    limit = filePartSize,
                    offset = offset
                });
            }
            catch (FileMigrationException ex)
            {
                var exportedAuth = await SendRequestAsync<TLExportedAuthorization>(new TLRequestExportAuthorization() { dc_id = ex.DC });

                var authKey = _session.AuthKey;
                var timeOffset = _session.TimeOffset;
                var serverAddress = _session.ServerAddress;
                var serverPort = _session.Port;

                await ReconnectToDcAsync(ex.DC);
                var auth = await SendRequestAsync<TLAuthorization>(new TLRequestImportAuthorization
                {
                    bytes = exportedAuth.bytes,
                    id = exportedAuth.id
                });
                result = await GetFile(location, filePartSize, offset);

                _session.AuthKey = authKey;
                _session.TimeOffset = timeOffset;
                _transport = new TcpTransport(serverAddress, serverPort);
                _session.ServerAddress = serverAddress;
                _session.Port = serverPort;
                await ConnectAsync();

            }

            return result;
        }

        public async Task SendPingAsync()
        {
            await _sender.SendPingAsync();
        }

        public async Task<TLAbsMessages> GetHistoryAsync(TLAbsInputPeer peer, int offset, int max_id, int limit)
        {
            if (!IsUserAuthorized())
                throw new InvalidOperationException("Authorize user first!");

            var req = new TLRequestGetHistory()
            {
                peer = peer,
                add_offset = offset,
                max_id = max_id,
                limit = limit
            };
            return await SendRequestAsync<TLAbsMessages>(req);
        }

        /// <summary>
        /// Serch user or chat. API: contacts.search#11f812d8 q:string limit:int = contacts.Found;
        /// </summary>
        /// <param name="q">User or chat name</param>
        /// <param name="limit">Max result count</param>
        /// <returns></returns>
        public async Task<TLFound> SearchUserAsync(string q, int limit = 10)
        {
            var r = new TeleSharp.TL.Contacts.TLRequestSearch
            {
                q = q,
                limit = limit
            };

            return await SendRequestAsync<TLFound>(r);
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
