using System;
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
using TeleSharp.TL;
using MD5 = System.Security.Cryptography.MD5;
using TeleSharp.TL.Help;
using TeleSharp.TL.Auth;
using TeleSharp.TL.Contacts;
using TeleSharp.TL.Messages;

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

        public TelegramClient(ISessionStore store, string sessionUserId, int apiId, string apiHash)
        {
            TLContext.Init();
            _apiHash = apiHash;
            _apiId = apiId;
            if (_apiId == 0)
                throw new InvalidOperationException("Your API_ID is invalid. Do a configuration first https://github.com/sochix/TLSharp#quick-configuration");

            if (string.IsNullOrEmpty(_apiHash))
                throw new InvalidOperationException("Your API_ID is invalid. Do a configuration first https://github.com/sochix/TLSharp#quick-configuration");
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
                var config = new TLRequestGetConfig() ;
                var request = new TLRequestInitConnection() { api_id = _apiId, app_version = "1.0.0", device_model = "PC", lang_code = "en", query= config, system_version = "Win 10.0" };
                var invokewithLayer = new TLRequestInvokeWithLayer() { layer = 53, query = request };
                await _sender.Send(invokewithLayer);
                await _sender.Receive(invokewithLayer);

                dcOptions = ((TLConfig)invokewithLayer.Response).dc_options.lists;
            }

            return true;
        }

        private async Task ReconnectToDc(int dcId)
        {
            if (dcOptions == null || !dcOptions.Any())
                throw new InvalidOperationException($"Can't reconnect. Establish initial connection first.");

            var dc = dcOptions.First(d => d.id == dcId);

            _transport = new TcpTransport(dc.ip_address, dc.port);
            _session.ServerAddress = dc.ip_address;
            _session.Port = dc.port;

            await Connect(true);
        }

        public bool IsUserAuthorized()
        {
            return _session.TLUser != null;
        }

        public async Task<bool> IsPhoneRegistered(string phoneNumber)
        {
            if (_sender == null)
                throw new InvalidOperationException("Not connected!");

            var authCheckPhoneRequest = new TLRequestCheckPhone() { phone_number = phoneNumber };
            await _sender.Send(authCheckPhoneRequest);
            await _sender.Receive(authCheckPhoneRequest);

            return authCheckPhoneRequest.Response.phone_registered;
        }

        public async Task<string> SendCodeRequest(string phoneNumber)
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

            return request.Response.phone_code_hash;
        }

        public async Task<TLUser> MakeAuth(string phoneNumber, string phoneCodeHash, string code)
        {
            var request = new TLRequestSignIn() { phone_number = phoneNumber, phone_code_hash = phoneCodeHash, phone_code = code };
            await _sender.Send(request);
            await _sender.Receive(request);

            OnUserAuthenticated(((TLUser)request.Response.user));

            return ((TLUser)request.Response.user);
        }

        public async Task<TLUser> SignUp(string phoneNumber, string phoneCodeHash, string code, string firstName, string lastName)
        {
            var request = new TLRequestSignUp() { phone_number = phoneNumber, phone_code = code, phone_code_hash = phoneCodeHash, first_name = firstName, last_name = lastName };
            await _sender.Send(request);
            await _sender.Receive(request);

            OnUserAuthenticated(((TLUser)request.Response.user));

            return ((TLUser)request.Response.user);
        }
        public async Task<T> SendRequest<T>(TLMethod methodtoExceute)
        {
            await _sender.Send(methodtoExceute);
            await _sender.Receive(methodtoExceute);

	        var result = methodtoExceute.GetType().GetProperty("Response").GetValue(methodtoExceute);
			
			return (T)result;
        }


	    public async Task<TLContacts> GetContacts()
	    {
		    if (!IsUserAuthorized())
				throw new InvalidOperationException("Authorize user first!");

			var req = new TLRequestGetContacts() {hash = ""};
			
			return await SendRequest<TLContacts>(req);
		}

	    public async Task<TLAbsUpdates> SendMessage(TLAbsInputPeer peer, string message)
	    {
			if (!IsUserAuthorized())
				throw new InvalidOperationException("Authorize user first!");

			long uniqueId = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds);

			return await SendRequest<TLAbsUpdates>(
				   new TLRequestSendMessage()
				   {
					   peer = peer,
					   message = message,
					   random_id = uniqueId
				   });
		}

	    public async Task<Boolean> SendTyping(TLAbsInputPeer peer)
	    {
			var req = new TLRequestSetTyping()
			{
				action = new TLSendMessageTypingAction(),
				peer = peer
			};
			return await SendRequest<Boolean>(req);
		}

	    public async Task<TLDialogs> GetUserDialogs()
	    {
			var peer = new TLInputPeerSelf();
			return await SendRequest<TLDialogs>(
				new TLRequestGetDialogs() { offset_date = 0, offset_peer = peer, limit = 100 });
		}

		private void OnUserAuthenticated(TLUser TLUser)
        {
            _session.TLUser = TLUser;
            _session.SessionExpires = int.MaxValue;

            _session.Save();
        }
       
    }
}
