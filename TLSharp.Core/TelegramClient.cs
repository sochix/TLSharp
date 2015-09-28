using System;
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
		private string _apiHash = "_your_hash";
		private int _apiId = 0;
		private Session _session;

		public TelegramClient(ISessionStore store, string sessionUserId)
		{
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
	}
}
