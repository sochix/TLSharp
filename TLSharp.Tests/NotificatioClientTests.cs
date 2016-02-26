using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TLSharp.Core;
using TLSharp.Core.Auth;
using TLSharp.Core.Network;

namespace TLSharp.Tests
{
    [TestClass]
	public class NotificatioClientTests
    {
        string NumberToAuthenticate { get; set; }
        string NumberToSendMessage { get; set; }

        int ApiID { get; set; }
        string ApiHash { get; set; }

        [TestInitialize]
		public void Init()
		{
			// Setup your phone numbers in app.config
			NumberToAuthenticate = ConfigurationManager.AppSettings["numberToAuthenticate"];
			if (string.IsNullOrEmpty(NumberToAuthenticate))
				throw new InvalidOperationException("NumberToAuthenticate is null");

            NumberToSendMessage = ConfigurationManager.AppSettings["numberToSendMessage"];
            if (string.IsNullOrEmpty(NumberToSendMessage))
                throw new InvalidOperationException("NumberToSendMessage is null");

            int apiId;
            if (!int.TryParse(ConfigurationManager.AppSettings["apiId"], out apiId))
                throw new InvalidOperationException("ApiID is invalid");
            ApiID = apiId;

            ApiHash = ConfigurationManager.AppSettings["apiHash"];
            if (string.IsNullOrEmpty(ApiHash))
                throw new InvalidOperationException("ApiHash is null");
        }
        
		[TestMethod] // first method to test
		public async Task AuthenticationWorks()
		{
			using (var transport = new TcpTransport("149.154.167.91", 443))
			{
				var authKey = await Authenticator.DoAuthentication(transport);

				Assert.IsNotNull(authKey.AuthKey.Data);
			}
        }

        [TestMethod] // second method to test
        public async Task TestConnection()
        {
            var store = new FakeSessionStore();
            var client = new TelegramClient(store, "", ApiID, ApiHash);

            Assert.IsTrue(await client.Connect());
        }

        [TestMethod] // third method to test
        public async Task AuthUser()
        {
            var store = new FileSessionStore();
            var client = new TelegramClient(store, "session", ApiID, ApiHash);

            await client.Connect();

            var hash = await client.SendCodeRequest(NumberToAuthenticate);
            var code = ""; // you can change code in debugger

            var user = await client.MakeAuth(NumberToAuthenticate, hash, code);

            Assert.IsNotNull(user);
        }

        [TestMethod] // fourth method to test (third is required)
        public async Task SendMessage()
        {
            // User should be already authenticated!
            var store = new FileSessionStore();
            var client = new TelegramClient(store, "session", ApiID, ApiHash);
            await client.Connect();

            Assert.IsTrue(client.IsUserAuthorized());

            var res = await client.ImportContacts(NumberToSendMessage);

            Assert.IsNotNull(res);

            await client.SendMessage(res[0], "Test message from TelegramClient");
        }



        [TestMethod] // alternative method
        public async Task CheckPhones()
        {
            var store = new FileSessionStore();
            var client = new TelegramClient(store, "session", ApiID, ApiHash);
            await client.Connect();

            var result = await client.IsPhoneRegistered(NumberToAuthenticate);
            Assert.IsTrue(result);
        }
    }
}
