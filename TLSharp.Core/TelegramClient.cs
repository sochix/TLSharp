using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLSharp.Core.Auth;
using TLSharp.Core.MTProto;
using TLSharp.Core.MTProto.Crypto;
using TLSharp.Core.Network;
using MD5 = System.Security.Cryptography.MD5;

namespace TLSharp.Core
{
    public class TelegramClient
    {
        private MtProtoSender _sender;
        private AuthKey _key;
        private TcpTransport _transport;
        private string _apiHash;
        private int _apiId;
        private Session _session;
        private List<DcOption> dcOptions;

        public User loggedUser { get { return _session.User; } }

        public List<Chat> chats;
        public List<User> users;

        public TelegramClient(ISessionStore store, string sessionUserId, int apiId, string apiHash)
        {
            if (apiId == 0)
                throw new InvalidOperationException("Your API_ID is invalid. Do a configuration first https://github.com/sochix/TLSharp#quick-configuration");
            _apiId = apiId;

            if (string.IsNullOrEmpty(apiHash))
                throw new InvalidOperationException("Your API_HASH is invalid. Do a configuration first https://github.com/sochix/TLSharp#quick-configuration");
            _apiHash = apiHash;

            _session = Session.TryLoadOrCreateNew(store, sessionUserId);
            _transport = new TcpTransport(_session.ServerAddress, _session.Port);
        }


        public async Task<bool> Connect(bool reconnect = false)
        {
            if (_session.AuthKey == null || reconnect)
            {
                var result = await Authenticator.DoAuthentication(_transport);
                _session.AuthKey = result.AuthKey;
                _session.TimeOffset = result.TimeOffset;
            }

            _sender = new MtProtoSender(_transport, _session);

            if (!reconnect)
            {
                var request = new TL.InvokeWithLayerRequest(47,
                    new TL.InitConnectionRequest(_apiId, "80EU", "Windows 10 Home", "0.9-BETA", "en",
                        new TL.HelpGetConfigRequest()));

                await _sender.Send(request);
                await _sender.Receive(request);
                
                var result = (TL.ConfigType)request.Result;
                dcOptions = result.DcOptions;
            }

            return true;
        }

        private async Task ReconnectToDc(int dcId)
        {
            if (dcOptions == null || !dcOptions.Any())
                throw new InvalidOperationException($"Can't reconnect. Establish initial connection first.");
            
            var dc = (TL.DcOptionType)dcOptions.First(d => ((TL.DcOptionType)d).Id == dcId);

            _transport = new TcpTransport(dc.IpAddress, dc.Port);
            _session.ServerAddress = dc.IpAddress;
            _session.Port = dc.Port;

            await Connect(true);
        }

        public bool IsUserAuthorized()
        {
            return _session.User != null;
        }

        public async Task<bool> IsPhoneRegistered(string phoneNumber)
        {
            if (_sender == null)
                throw new InvalidOperationException("Not connected!");

            var authCheckPhoneRequest = new TL.AuthCheckPhoneRequest(phoneNumber);
            await _sender.Send(authCheckPhoneRequest);
            await _sender.Receive(authCheckPhoneRequest);

            var result = (TL.AuthCheckedPhoneType)authCheckPhoneRequest.Result;
            return result.PhoneRegistered;
        }

        public async Task<string> SendCodeRequest(string phoneNumber)
        {
            var completed = false;

            TL.AuthSendCodeRequest request = null;

            while (!completed)
            {
                request = new TL.AuthSendCodeRequest(phoneNumber, 5, _apiId, _apiHash, "en");
                try
                {
                    await _sender.Send(request);
                    await _sender.Receive(request);

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
            // TODO handle other types (such as SMS)
            var result = (TL.AuthSentAppCodeType)request.Result;
            return result.PhoneCodeHash;
        }

        public async Task<User> MakeAuth(string phoneNumber, string phoneHash, string code)
        {
            var request = new TL.AuthSignInRequest(phoneNumber, phoneHash, code);
            await _sender.Send(request);
            await _sender.Receive(request);

            var result = (TL.AuthAuthorizationType)request.Result;
            _session.User = result.User;

            _session.Save();

            return result.User;
        }


        public async Task<List<User>> ImportContacts(params string[] phoneNumbers)
        {
            var contacts = new List<InputContact>(phoneNumbers.Length);
            foreach (var phone in phoneNumbers)
                contacts.Add(new TL.InputPhoneContactType(0, phone, "Test Name " + phone, string.Empty));

            var request = new TL.ContactsImportContactsRequest(contacts, false);
            await _sender.Send(request);
            await _sender.Receive(request);

            var result = (TL.ContactsImportedContactsType)request.Result;

            return result.Users;
        }

        public async Task<bool> SendMessage(User user, string message)
        {
            InputPeer peer;
            if (user is TL.UserType)
            {
                var userType = (TL.UserType)user;
                peer = new TL.InputPeerUserType(userType.Id, userType.AccessHash ?? 0);
            }
            else
                return false;

            var request = new TL.MessagesSendMessageRequest(null, null, peer, null, message, getRandomLong(), null, null);

            await _sender.Send(request);
            await _sender.Receive(request);

            return true;
        }

        static readonly Random r = new Random();
        static long getRandomLong()
        {
            var buffer = new byte[sizeof(long)];
            r.NextBytes(buffer);
            return BitConverter.ToInt64(buffer, 0);
        }
    }
}
