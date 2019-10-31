using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeleSharp.TL;
using TeleSharp.TL.Account;
using TeleSharp.TL.Auth;
using TeleSharp.TL.Contacts;
using TeleSharp.TL.Help;
using TeleSharp.TL.Messages;
using TeleSharp.TL.Upload;
using TLSharp.Core.Auth;
using TLSharp.Core.Network;
using TLSharp.Core.Utils;
using TLAuthorization = TeleSharp.TL.Auth.TLAuthorization;

namespace TLSharp.Core
{
    public class TelegramClient : IDisposable
    {
        private MtProtoSender sender;
        private TcpTransport transport;
        private readonly string apiHash = "";
        private readonly int apiId;
        private List<TLDcOption> dcOptions;
        private readonly TcpClientConnectionHandler handler;

        public Session Session { get; }

        public TelegramClient(int apiId, string apiHash,
            ISessionStore store = null, string sessionUserId = "session", TcpClientConnectionHandler handler = null)
        {
            if (apiId == default(int))
                throw new MissingApiConfigurationException("API_ID");
            if (string.IsNullOrEmpty(apiHash))
                throw new MissingApiConfigurationException("API_HASH");

            if (store == null)
                store = new FileSessionStore();

            this.apiHash = apiHash;
            this.apiId = apiId;
            this.handler = handler;

            Session = Session.TryLoadOrCreateNew(store, sessionUserId);
            transport = new TcpTransport(Session.DataCenter.Address, Session.DataCenter.Port, this.handler);
        }

       
        public async Task ConnectAsync(bool reconnect = false, CancellationToken token = default(CancellationToken))
        {
            token.ThrowIfCancellationRequested();
            
            if (Session.AuthKey == null || reconnect)
            {
                var result = await Authenticator.DoAuthentication(transport, token).ConfigureAwait(false);
                Session.AuthKey = result.AuthKey;
                Session.TimeOffset = result.TimeOffset;
            }

            sender = new MtProtoSender(transport, Session);

            //set-up layer
            var config = new TLRequestGetConfig();
            var request = new TLRequestInitConnection()
            {
                ApiId = apiId,
                AppVersion = "1.0.0",
                DeviceModel = "PC",
                LangCode = "en",
                Query = config,
                SystemVersion = "Win 10.0"
            };
            var invokewithLayer = new TLRequestInvokeWithLayer() { Layer = 66, Query = request };
            await sender.Send(invokewithLayer, token).ConfigureAwait(false);
            await sender.Receive(invokewithLayer, token).ConfigureAwait(false);

            dcOptions = ((TLConfig)invokewithLayer.Response).DcOptions.ToList();
        }

        private async Task ReconnectToDcAsync(int dcId, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            
            if (dcOptions == null || !dcOptions.Any())
                throw new InvalidOperationException($"Can't reconnect. Establish initial connection first.");

            TLExportedAuthorization exported = null;
            if (Session.TLUser != null)
            {
                TLRequestExportAuthorization exportAuthorization = new TLRequestExportAuthorization() { DcId = dcId };
                exported = await SendRequestAsync<TLExportedAuthorization>(exportAuthorization, token).ConfigureAwait(false);
            }

            var dc = dcOptions.First(d => d.Id == dcId);
            var dataCenter = new DataCenter (dcId, dc.IpAddress, dc.Port);

            transport = new TcpTransport(dc.IpAddress, dc.Port, handler);
            Session.DataCenter = dataCenter;

            await ConnectAsync(true, token).ConfigureAwait(false);

            if (Session.TLUser != null)
            {
                TLRequestImportAuthorization importAuthorization = new TLRequestImportAuthorization() { Id = exported.Id, Bytes = exported.Bytes };
                var imported = await SendRequestAsync<TLAuthorization>(importAuthorization, token).ConfigureAwait(false);
                OnUserAuthenticated(((TLUser)imported.User));
            }
        }

        private async Task RequestWithDcMigration(TLMethod request, CancellationToken token)
        {
            if (sender == null)
                throw new InvalidOperationException("Not connected!");

            var completed = false;
            while(!completed)
            {
                try
                {
                    await sender.Send(request, token).ConfigureAwait(false);
                    await sender.Receive(request, token).ConfigureAwait(false);
                    completed = true;
                }
                catch(DataCenterMigrationException e)
                {
                    if (Session.DataCenter.DataCenterId.HasValue &&
                        Session.DataCenter.DataCenterId.Value == e.DC)
                    {
                        throw new Exception($"Telegram server replied requesting a migration to DataCenter {e.DC} when this connection was already using this DataCenter", e);
                    }

                    await ReconnectToDcAsync(e.DC, token).ConfigureAwait(false);
                    // prepare the request for another try
                    request.ConfirmReceived = false;
                }
            }
        }

        public bool IsUserAuthorized()
        {
            return Session.TLUser != null;
        }

        public async Task<bool> IsPhoneRegisteredAsync(string phoneNumber, CancellationToken token = default(CancellationToken))
        {
            if (String.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentNullException(nameof(phoneNumber));

            var authCheckPhoneRequest = new TLRequestCheckPhone() { PhoneNumber = phoneNumber };

            await RequestWithDcMigration(authCheckPhoneRequest, token).ConfigureAwait(false);

            return authCheckPhoneRequest.Response.PhoneRegistered;
        }

        public async Task<string> SendCodeRequestAsync(string phoneNumber, CancellationToken token = default(CancellationToken))
        {
            if (String.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentNullException(nameof(phoneNumber));

            var request = new TLRequestSendCode() { PhoneNumber = phoneNumber, ApiId = apiId, ApiHash = apiHash };

            await RequestWithDcMigration(request, token).ConfigureAwait(false);

            return request.Response.PhoneCodeHash;
        }

        public async Task<TLUser> MakeAuthAsync(string phoneNumber, string phoneCodeHash, string code, CancellationToken token = default(CancellationToken))
        {
            if (String.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentNullException(nameof(phoneNumber));

            if (String.IsNullOrWhiteSpace(phoneCodeHash))
                throw new ArgumentNullException(nameof(phoneCodeHash));

            if (String.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException(nameof(code));
            
            var request = new TLRequestSignIn() { PhoneNumber = phoneNumber, PhoneCodeHash = phoneCodeHash, PhoneCode = code };

            await RequestWithDcMigration(request, token).ConfigureAwait(false);

            OnUserAuthenticated(((TLUser)request.Response.User));

            return ((TLUser)request.Response.User);
        }

        public async Task<TLPassword> GetPasswordSetting(CancellationToken token = default(CancellationToken))
        {
            var request = new TLRequestGetPassword();

            await RequestWithDcMigration(request, token).ConfigureAwait(false);

            return ((TLPassword)request.Response);
        }

        public async Task<TLUser> MakeAuthWithPasswordAsync(TLPassword password, string password_str, CancellationToken token = default(CancellationToken))
        {
            token.ThrowIfCancellationRequested();

            byte[] password_Bytes = Encoding.UTF8.GetBytes(password_str);
            IEnumerable<byte> rv = password.CurrentSalt.Concat(password_Bytes).Concat(password.CurrentSalt);

            SHA256Managed hashstring = new SHA256Managed();
            var password_hash = hashstring.ComputeHash(rv.ToArray());

            var request = new TLRequestCheckPassword() { PasswordHash = password_hash };

            await RequestWithDcMigration(request, token).ConfigureAwait(false);

            OnUserAuthenticated(((TLUser)request.Response.User));

            return ((TLUser)request.Response.User);
        }

        public async Task<TLUser> SignUpAsync(string phoneNumber, string phoneCodeHash, string code, string firstName, string lastName, CancellationToken token = default(CancellationToken))
        {
            var request = new TLRequestSignUp() { PhoneNumber = phoneNumber, PhoneCode = code, PhoneCodeHash = phoneCodeHash, FirstName = firstName, LastName = lastName };
            
            await RequestWithDcMigration(request, token).ConfigureAwait(false);

            OnUserAuthenticated(((TLUser)request.Response.User));

            return ((TLUser)request.Response.User);
        }

        public async Task<T> SendRequestAsync<T>(TLMethod methodToExecute, CancellationToken token = default(CancellationToken))
        {
            await RequestWithDcMigration(methodToExecute, token).ConfigureAwait(false);

            var result = methodToExecute.GetType().GetProperty("Response").GetValue(methodToExecute);

            return (T)result;
        }

        public async Task<TLContacts> GetContactsAsync(CancellationToken token = default(CancellationToken))
        {
            if (!IsUserAuthorized())
                throw new InvalidOperationException("Authorize user first!");

            var req = new TLRequestGetContacts() { Hash = "" };

            return await SendRequestAsync<TLContacts>(req, token).ConfigureAwait(false);
        }

        public async Task<TLAbsUpdates> SendMessageAsync(TLAbsInputPeer peer, string message, CancellationToken token = default(CancellationToken))
        {
            if (!IsUserAuthorized())
                throw new InvalidOperationException("Authorize user first!");

            return await SendRequestAsync<TLAbsUpdates>(
                   new TLRequestSendMessage()
                   {
                       Peer = peer,
                       Message = message,
                       RandomId = Helpers.GenerateRandomLong()
                   }, token).ConfigureAwait(false);
        }

        public async Task<Boolean> SendTypingAsync(TLAbsInputPeer peer, CancellationToken token = default(CancellationToken))
        {
            var req = new TLRequestSetTyping()
            {
                Action = new TLSendMessageTypingAction(),
                Peer = peer
            };
            return await SendRequestAsync<Boolean>(req, token).ConfigureAwait(false);
        }

        public async Task<TLAbsDialogs> GetUserDialogsAsync(int offsetDate = 0, int offsetId = 0, TLAbsInputPeer offsetPeer = null, int limit = 100,
            CancellationToken token = default(CancellationToken))
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
            return await SendRequestAsync<TLAbsDialogs>(req, token).ConfigureAwait(false);
        }

        public async Task<TLAbsUpdates> SendUploadedPhoto(TLAbsInputPeer peer, TLAbsInputFile file, string caption, CancellationToken token = default(CancellationToken))
        {
            return await SendRequestAsync<TLAbsUpdates>(new TLRequestSendMedia()
            {
                RandomId = Helpers.GenerateRandomLong(),
                Background = false,
                ClearDraft = false,
                Media = new TLInputMediaUploadedPhoto() { File = file, Caption = caption },
                Peer = peer
            }, token).ConfigureAwait(false);
        }

        public async Task<TLAbsUpdates> SendUploadedDocument(TLAbsInputPeer peer, TLAbsInputFile file, string caption, string mimeType, TLVector<TLAbsDocumentAttribute> attributes,
            CancellationToken token = default(CancellationToken))
        {
            return await SendRequestAsync<TLAbsUpdates>(new TLRequestSendMedia()
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
            }, token).ConfigureAwait(false);
        }

        public async Task<TLFile> GetFile(TLAbsInputFileLocation location, int filePartSize, int offset = 0, CancellationToken token = default(CancellationToken))
        {
            var result = await SendRequestAsync<TLFile>(new TLRequestGetFile()
            {
                Location = location,
                Limit = filePartSize,
                Offset = offset
            }, token).ConfigureAwait(false);
            return result;
        }

        public async Task SendPingAsync(CancellationToken token = default(CancellationToken))
        {
            await sender.SendPingAsync(token).ConfigureAwait(false);
        }

        public async Task<TLAbsMessages> GetHistoryAsync(TLAbsInputPeer peer, int offsetId = 0, int offsetDate = 0, int addOffset = 0, int limit = 100, int maxId = 0,
            int minId = 0, CancellationToken token = default(CancellationToken))
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
            return await SendRequestAsync<TLAbsMessages>(req, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Serch user or chat. API: contacts.search#11f812d8 q:string limit:int = contacts.Found;
        /// </summary>
        /// <param name="q">User or chat name</param>
        /// <param name="limit">Max result count</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TLFound> SearchUserAsync(string q, int limit = 10, CancellationToken token = default(CancellationToken))
        {
            var r = new TeleSharp.TL.Contacts.TLRequestSearch
            {
                Q = q,
                Limit = limit
            };

            return await SendRequestAsync<TLFound>(r, token).ConfigureAwait(false);
        }

        private void OnUserAuthenticated(TLUser TLUser)
        {
            Session.TLUser = TLUser;
            Session.SessionExpires = int.MaxValue;

            Session.Save();
        }

        public bool IsConnected
        {
            get
            {
                if (transport == null)
                    return false;
                return transport.IsConnected;
            }
        }

        public void Dispose()
        {
            if (transport != null)
            {
                transport.Dispose();
                transport = null;
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
