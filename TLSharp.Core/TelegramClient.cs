﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeleSharp.TL;
using TeleSharp.TL.Account;
using TeleSharp.TL.Auth;
using TeleSharp.TL.Channels;
using TeleSharp.TL.Contacts;
using TeleSharp.TL.Help;
using TeleSharp.TL.Messages;
using TeleSharp.TL.Upload;
using TLSharp.Core.Auth;
using TLSharp.Core.Exceptions;
using TLSharp.Core.MTProto.Crypto;
using TLSharp.Core.Network;
using TLSharp.Core.Network.Exceptions;
using TLSharp.Core.Utils;
using TLAuthorization = TeleSharp.TL.Auth.TLAuthorization;

namespace TLSharp.Core
{
    public class TelegramClient : IDisposable
    {
        public const int DEFAULT_PAGE_SIZE = 200;

        private MtProtoSender sender;
        private TcpTransport transport;
        private string apiHash = String.Empty;
        private int apiId = 0;
        private Session session;
        private List<TLDcOption> dcOptions;
        private TcpClientConnectionHandler handler;

        public Session Session
        {
            get { return session; }
        }

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

            session = Session.TryLoadOrCreateNew(store, sessionUserId);
            transport = new TcpTransport (session.DataCenter.Address, session.DataCenter.Port, this.handler);
        }

        public async Task ConnectAsync(bool reconnect = false, CancellationToken token = default(CancellationToken))
        {
            token.ThrowIfCancellationRequested();

            if (session.AuthKey == null || reconnect)
            {
                var result = await Authenticator.DoAuthentication(transport, token).ConfigureAwait(false);
                session.AuthKey = result.AuthKey;
                session.TimeOffset = result.TimeOffset;
            }

            sender = new MtProtoSender(transport, session);

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

        private async Task ReconnectToDcAsync(int dcId, CancellationToken token = default(CancellationToken))
        {
            token.ThrowIfCancellationRequested();

            if (dcOptions == null || !dcOptions.Any())
                throw new InvalidOperationException($"Can't reconnect. Establish initial connection first.");

            TLExportedAuthorization exported = null;
            if (session.TLUser != null)
            {
                TLRequestExportAuthorization exportAuthorization = new TLRequestExportAuthorization() { DcId = dcId };
                exported = await SendRequestAsync<TLExportedAuthorization>(exportAuthorization, token).ConfigureAwait(false);
            }

            var dc = dcOptions.First(d => d.Id == dcId);
            var dataCenter = new DataCenter (dcId, dc.IpAddress, dc.Port);

            transport = new TcpTransport(dc.IpAddress, dc.Port, handler);
            session.DataCenter = dataCenter;

            await ConnectAsync(true, token).ConfigureAwait(false);

            if (session.TLUser != null)
            {
                TLRequestImportAuthorization importAuthorization = new TLRequestImportAuthorization() { Id = exported.Id, Bytes = exported.Bytes };
                var imported = await SendRequestAsync<TLAuthorization>(importAuthorization, token).ConfigureAwait(false);
                OnUserAuthenticated((TLUser)imported.User);
            }
        }

        private async Task RequestWithDcMigration(TLMethod request, CancellationToken token = default(CancellationToken))
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
                    if (session.DataCenter.DataCenterId.HasValue &&
                        session.DataCenter.DataCenterId.Value == e.DC)
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
            return session.TLUser != null;
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

            return (TLPassword)request.Response;
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

            OnUserAuthenticated((TLUser)request.Response.User);

            return (TLUser)request.Response.User;
        }

        public async Task<TLUser> SignUpAsync(string phoneNumber, string phoneCodeHash, string code, string firstName, string lastName, CancellationToken token = default(CancellationToken))
        {
            var request = new TLRequestSignUp() { PhoneNumber = phoneNumber, PhoneCode = code, PhoneCodeHash = phoneCodeHash, FirstName = firstName, LastName = lastName };
            
            await RequestWithDcMigration(request, token).ConfigureAwait(false);

            OnUserAuthenticated((TLUser)request.Response.User);

            return (TLUser)request.Response.User;
        }

        public async Task<T> SendRequestAsync<T>(TLMethod methodToExecute, CancellationToken token = default(CancellationToken))
        {
            await RequestWithDcMigration(methodToExecute, token).ConfigureAwait(false);

            var result = methodToExecute.GetType().GetProperty("Response").GetValue(methodToExecute);

            return (T)result;
        }

        internal async Task<T> SendAuthenticatedRequestAsync<T> (TLMethod methodToExecute, CancellationToken token = default(CancellationToken))
        {
            if (!IsUserAuthorized())
                throw new InvalidOperationException("Authorize user first!");

            return await SendRequestAsync<T>(methodToExecute, token)
                .ConfigureAwait(false);
        }

        public async Task<TLUser> UpdateUsernameAsync(string username, CancellationToken token = default(CancellationToken))
        {
            var req = new TLRequestUpdateUsername { Username = username };

            return await SendAuthenticatedRequestAsync<TLUser>(req, token)
                .ConfigureAwait(false);
        }

        public async Task<bool> CheckUsernameAsync(string username, CancellationToken token = default(CancellationToken))
        {
            var req = new TLRequestCheckUsername { Username = username };

            return await SendAuthenticatedRequestAsync<bool>(req, token)
                .ConfigureAwait(false);
        }

        public async Task<TLImportedContacts> ImportContactsAsync(IReadOnlyList<TLInputPhoneContact> contacts, CancellationToken token = default(CancellationToken))
        {
            var req = new TLRequestImportContacts { Contacts = new TLVector<TLInputPhoneContact>(contacts)};

            return await SendAuthenticatedRequestAsync<TLImportedContacts>(req, token)
                .ConfigureAwait(false);
        }

        public async Task<bool> DeleteContactsAsync(IReadOnlyList<TLAbsInputUser> users, CancellationToken token = default(CancellationToken))
        {
            var req = new TLRequestDeleteContacts {Id = new TLVector<TLAbsInputUser>(users)};

            return await SendAuthenticatedRequestAsync<bool>(req, token)
                .ConfigureAwait(false);
        }

        public async Task<TLLink> DeleteContactAsync(TLAbsInputUser user, CancellationToken token = default(CancellationToken))
        {
            var req = new TLRequestDeleteContact {Id = user};

            return await SendAuthenticatedRequestAsync<TLLink>(req, token)
                .ConfigureAwait(false);
        }

        public async Task<TLContacts> GetContactsAsync(CancellationToken token = default(CancellationToken))
        {
            var req = new TLRequestGetContacts() { Hash = "" };

            return await SendAuthenticatedRequestAsync<TLContacts>(req, token)
                .ConfigureAwait(false);
        }

        public async Task<TLAbsUpdates> SendMessageAsync(TLAbsInputPeer peer, string message, CancellationToken token = default(CancellationToken))
        {
            return await SendAuthenticatedRequestAsync<TLAbsUpdates>(
                    new TLRequestSendMessage()
                    {
                        Peer = peer,
                        Message = message,
                        RandomId = Helpers.GenerateRandomLong()
                    }, token)
                .ConfigureAwait(false);
        }

        public async Task<Boolean> SendTypingAsync(TLAbsInputPeer peer, CancellationToken token = default(CancellationToken))
        {
            var req = new TLRequestSetTyping()
            {
                Action = new TLSendMessageTypingAction(),
                Peer = peer
            };
            return await SendAuthenticatedRequestAsync<Boolean>(req, token)
                .ConfigureAwait(false);
        }

        public async Task<TLAbsDialogs> GetUserDialogsAsync(int offsetDate = 0, int offsetId = 0, TLAbsInputPeer offsetPeer = null, int limit = 100, CancellationToken token = default(CancellationToken))
        {
            if (offsetPeer == null)
                offsetPeer = new TLInputPeerSelf();

            var req = new TLRequestGetDialogs()
            { 
                OffsetDate = offsetDate, 
                OffsetId = offsetId, 
                OffsetPeer = offsetPeer, 
                Limit = limit
            };
            return await SendAuthenticatedRequestAsync<TLAbsDialogs>(req, token)
                .ConfigureAwait(false);
        }

        public async Task<TLAbsUpdates> SendUploadedPhoto(TLAbsInputPeer peer, TLAbsInputFile file, string caption, CancellationToken token = default(CancellationToken))
        {
            return await SendAuthenticatedRequestAsync<TLAbsUpdates>(new TLRequestSendMedia()
                {
                    RandomId = Helpers.GenerateRandomLong(),
                    Background = false,
                    ClearDraft = false,
                    Media = new TLInputMediaUploadedPhoto() { File = file, Caption = caption },
                    Peer = peer
                }, token)
                .ConfigureAwait(false);
        }

        public async Task<TLAbsUpdates> SendUploadedDocument(
            TLAbsInputPeer peer, TLAbsInputFile file, string caption, string mimeType, TLVector<TLAbsDocumentAttribute> attributes, CancellationToken token = default(CancellationToken))
        {
            return await SendAuthenticatedRequestAsync<TLAbsUpdates>(new TLRequestSendMedia()
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
                }, token)
                .ConfigureAwait(false);
        }

        public async Task<TLFile> GetFile(TLAbsInputFileLocation location, int filePartSize, int offset = 0, CancellationToken token = default(CancellationToken))
        {
            TLFile result = await SendAuthenticatedRequestAsync<TLFile>(new TLRequestGetFile
                {
                    Location = location,
                    Limit = filePartSize,
                    Offset = offset
                }, token)
                .ConfigureAwait(false);
            return result;
        }

        public async Task SendPingAsync(CancellationToken token = default(CancellationToken))
        {
            await sender.SendPingAsync(token)
                .ConfigureAwait(false);
        }

        public async Task<TLAbsMessages> GetHistoryAsync(TLAbsInputPeer peer, int offsetId = 0, int offsetDate = 0, int addOffset = 0, int limit = 100, int maxId = 0, int minId = 0, CancellationToken token = default(CancellationToken))
        {
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
            return await SendAuthenticatedRequestAsync<TLAbsMessages>(req, token)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Authenticates a Bot 
        /// </summary>
        /// <param name="botAuthToken">The token of the bot to authenticate</param>
        /// <param name="token"></param>
        /// <returns>The TLUser descriptor</returns>
        public async Task<TLUser> MakeAuthBotAsync(string botAuthToken, CancellationToken token = default(CancellationToken))
        {
            if (String.IsNullOrWhiteSpace(botAuthToken))
            {
                throw new ArgumentNullException(nameof(botAuthToken));
            }

            var request = new TLRequestImportBotAuthorization() { BotAuthToken = botAuthToken, ApiId = apiId, ApiHash = apiHash };

            await RequestWithDcMigration(request, token).ConfigureAwait(false);

            OnUserAuthenticated(((TLUser)request.Response.User));

            return ((TLUser)request.Response.User);
        }

        /// <summary>
        /// Gets the list of chats and channels opened by the authenticated user. 
        /// Throws an exception if the authenticated user is a bot.
        /// </summary> 
        /// <param name="token"></param>
        /// <returns>The list of chats opened by the authenticated user</returns>
        public async Task<TLChats> GetAllChats(CancellationToken token = default(CancellationToken))
        {
            return await GetAllChats(null, token);
        }
        /// <summary>
        /// Gets the list of chats and channels opened by the authenticated user except the passed ones. 
        /// Throws an exception if the authenticated user is a bot.
        /// </summary> 
        /// <param name="ids">The IDs of the chats to be returned</param>
        /// <param name="token"></param>
        /// <returns>The list of chats opened by the authenticated user</returns>
        public async Task<TLChats> GetAllChats(int[] exceptdIds, CancellationToken token = default(CancellationToken))
        {
            var ichats = new TeleSharp.TL.TLVector<int>(); // we can't pass a null argument to the TLRequestGetChats
            if (exceptdIds != null)
                Array.ForEach(exceptdIds, x => ichats.Add(x));
            var chats = await SendRequestAsync<TLChats>(new TLRequestGetAllChats() { ExceptIds = ichats }).ConfigureAwait(false);
            return chats;
        }
        /// <summary>
        /// Gets the information about a channel
        /// </summary>
        /// <param name="channel">The channel to get the info of</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TeleSharp.TL.Messages.TLChatFull> GetFullChannel(TLChannel channel, CancellationToken token = default(CancellationToken))
        {
            if (channel == null) return null;
            return await GetFullChannel(channel.Id, (long)channel.AccessHash, token).ConfigureAwait(false);
        }
        /// <summary>
        /// Gets the information about a channel
        /// </summary>
        /// <param name="channelId">The ID of the channel</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TeleSharp.TL.Messages.TLChatFull> GetFullChannel(int channelId, long accessHash, CancellationToken token = default(CancellationToken))
        {
            var req = new TLRequestGetFullChannel() { Channel = new TLInputChannel() { ChannelId = channelId, AccessHash = accessHash } };
            var fchat = await SendRequestAsync<TeleSharp.TL.Messages.TLChatFull>(req).ConfigureAwait(false);

            return fchat;
        }
        /// <summary>
        /// Gets the channels having the supplied IDs
        /// </summary>
        /// <param name="channelIds">The IDs of the channels to be retrieved</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TeleSharp.TL.Messages.TLChats> GetChannels(int[] channelIds, CancellationToken token = default(CancellationToken))
        {
            var channels = new TLVector<TeleSharp.TL.TLAbsInputChannel>(); // we can't pass a null argument to the TLRequestGetChats
            if (channelIds != null)
                Array.ForEach(channelIds, x => channels.Add(new TLInputChannel() { ChannelId = x }));
            var req = new TLRequestGetChannels() { Id = channels };
            var fchat = await SendRequestAsync<TeleSharp.TL.Messages.TLChats>(req).ConfigureAwait(false);

            return fchat;
        }
        /// <summary>
        /// Gets the participants of the channel having the supplied type.
        /// The method will auto-paginate results and return all the participants
        /// </summary>
        /// <param name="channel">The TLChannel whose participants are requested</param>
        /// <param name="stIdx">The index to start fetching from. -1 will automatically fetch all the results</param>
        /// <param name="stIdx">The index to start fetching from. -1 will automatically fetch all the results</param>
        /// <param name="pageSize">How many results are needed. How many results to be fetch on each iteration. 
        /// Values smaller than 0 are ignored. If stIdx manually set, a number of results smaller than pageSize might be returned by Telegram.</param>
        /// <param name="partType">The type of the participants to get. Choose Recents not to filter</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TLChannelParticipants> GetParticipants(TLChannel channel, int stIdx = -1, int pageSize = -1, ParticipantTypes partType = ParticipantTypes.Recents, CancellationToken token = default(CancellationToken))
        {
            if (channel == null) return null;
            return await GetParticipants(channel.Id, (long)channel.AccessHash, stIdx, pageSize, partType, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the participants of the channel having the supplied type.
        /// The method will auto-paginate results and return all the participants
        /// </summary>
        /// <param name="channelId">The id of the channel whose participants are requested</param>
        /// <param name="accessHash">The access hash of the channel whose participants are requested</param>
        /// <param name="stIdx">The index to start fetching from. -1 will automatically fetch all the results</param>
        /// <param name="pageSize">How many results are needed. How many results to be fetch on each iteration. 
        /// Values smaller than 0 are ignored. If stIdx manually set, a number of results smaller than pageSize might be returned by Telegram.</param>
        /// <param name="partType">The type of the participants to get. Choose Recents not to filter</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TLChannelParticipants> GetParticipants(int channelId, long accessHash, int stIdx = -1, int pageSize = -1, ParticipantTypes partType = ParticipantTypes.Recents, CancellationToken token = default(CancellationToken))
        {
            TLAbsChannelParticipantsFilter filter;
            switch (partType)
            {
                case ParticipantTypes.Admins:
                    filter = new TLChannelParticipantsAdmins();
                    break;

                case ParticipantTypes.Kicked:
                    filter = new TLChannelParticipantsKicked();
                    break;

                case ParticipantTypes.Bots:
                    filter = new TLChannelParticipantsBots();
                    break;

                case ParticipantTypes.Recents:
                    filter = new TLChannelParticipantsRecent();
                    break;

                case ParticipantTypes.Banned:
                case ParticipantTypes.Restricted:
                case ParticipantTypes.Contacts:
                case ParticipantTypes.Search:
                default:
                    throw new NotImplementedException($"{partType} not implemented yet");
            }

            int total = 0;
            int found = stIdx < 0 ? 0 : stIdx;
            pageSize = pageSize < 0 ? DEFAULT_PAGE_SIZE : pageSize;

            List<TLChannelParticipants> results = new List<TLChannelParticipants>();
            TLChannelParticipants ret = new TLChannelParticipants();
            ret.Participants = new TLVector<TLAbsChannelParticipant>();
            ret.Users = new TLVector<TLAbsUser>();

            do
            {
                var req = new TLRequestGetParticipants()
                {
                    Channel = new TLInputChannel()
                    {
                        ChannelId = channelId,
                        AccessHash = accessHash
                    },
                    Filter = new TLChannelParticipantsRecent(),
                    Offset = found,
                    Limit = pageSize
                };
                var fchat = await SendRequestAsync<TLChannelParticipants>(req).ConfigureAwait(false);
                total = fchat.Count;
                found += fchat.Participants.Count;
                results.Add(fchat);
                foreach (var p in fchat.Participants)
                    ret.Participants.Add(p);
                foreach (var u in fchat.Users)
                    ret.Users.Add(u);
            } while (found < total && stIdx == -1);
            ret.Count = ret.Participants.Count;
            return ret;
        }

        /// <summary>
        /// Serch user or chat. API: contacts.search#11f812d8 q:string limit:int = contacts.Found;
        /// </summary>
        /// <param name="q">User or chat name</param>
        /// <param name="limit">Max result count</param>
        /// <returns></returns>
        public async Task<TLFound> SearchUserAsync(string q, int limit = 10, CancellationToken token = default(CancellationToken))
        {
            var r = new TeleSharp.TL.Contacts.TLRequestSearch
            {
                Q = q,
                Limit = limit
            };

            return await SendAuthenticatedRequestAsync<TLFound>(r, token)
                .ConfigureAwait(false);
        }

        private void OnUserAuthenticated(TLUser TLUser)
        {
            session.TLUser = TLUser;
            session.SessionExpires = int.MaxValue;

            session.Save();
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
}
