using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TLSharp.Core.Auth;
using TLSharp.Core.MTProto;
using TLSharp.Core.MTProto.Crypto;
using TLSharp.Core.Network;
using TLSharp.Core.Requests;

namespace TLSharp.Core
{
	public class TelegramClient
	{
		private MtProtoSender _sender;
		private AuthKey _key;
		private readonly TcpTransport _transport;
		private string _apiHash = "a2514f96431a228e4b9ee473f6c51945";
		private int _apiId = 19474;
		private Session _session;

        public User loggedUser { get { return _session.User; } }

        public List<Chat> chats;
        public List<User> users;

        public TelegramClient(ISessionStore store, string sessionUserId)
		{
			if (_apiId == 0)
				throw new InvalidOperationException("Your API_ID is invalid. Do a configuration first https://github.com/sochix/TLSharp#quick-configuration");

			if (string.IsNullOrEmpty(_apiHash))
				throw new InvalidOperationException("Your API_ID is invalid. Do a configuration first https://github.com/sochix/TLSharp#quick-configuration");

			_transport = new TcpTransport();
			_session = Session.TryLoadOrCreateNew(store, sessionUserId);
		}

		public async Task<bool> Connect()
		{
			if (_session.AuthKey == null)
			{
				var result = await Authenticator.DoAuthentication(_transport);
				_session.AuthKey = result.AuthKey;
				_session.TimeOffset = result.TimeOffset;
			}
				

			_sender = new MtProtoSender(_transport, _session);

			var request = new InitConnectionRequest(_apiId);

			await _sender.Send(request);
			await _sender.Recieve(request);

			return true;
		}

		public bool IsUserAuthorized()
		{
			return _session.User != null;
		}

		public async Task<bool> IsPhoneRegistered(string phoneNumber)
		{
			if (_sender == null)
				throw new InvalidOperationException("Not connected!");

			var authCheckPhoneRequest = new AuthCheckPhoneRequest(phoneNumber);
			await _sender.Send(authCheckPhoneRequest);
			await _sender.Recieve(authCheckPhoneRequest);

			return authCheckPhoneRequest._phoneRegistered;
		}

		public async Task<string> SendCodeRequest(string phoneNumber)
		{
			var request = new AuthSendCodeRequest(phoneNumber, 5, _apiId, _apiHash, "en");
			await _sender.Send(request);
			await _sender.Recieve(request);

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

		public async Task<int?> ImportContact(string phoneNumber)
		{
			var request = new ImportContactRequest(new InputPhoneContactConstructor(0, phoneNumber, "My Test Name", string.Empty));
			await _sender.Send(request);
			await _sender.Recieve(request);

			var importedUser = request.users.FirstOrDefault();

			return importedUser == null ? (int?) null : ((UserContactConstructor) importedUser).id;
		}

		public async Task SendMessage(int id, string message)
		{
			var request = new SendMessageRequest(new InputPeerContactConstructor(id), message );

			await _sender.Send(request);
			await _sender.Recieve(request);
		}

        public async Task LoadChatsAndUsers(int offset, int max_id, int limit)
        {
            // GetDialogs
            var request = new GetDialogsRequest(offset, max_id, limit);
            await _sender.Send(request);
            await _sender.Recieve(request);

            chats = request.chats;
            users = request.users;
        }

        public async Task<List<Message>> GetHistory(int user_id, int offset, int max_id, int limit)
        {
            var request = new GetHistoryRequest(new InputPeerContactConstructor(user_id), offset, max_id, limit);
            await _sender.Send(request);
            await _sender.Recieve(request);

            return request.messages;
        }
    }
}
