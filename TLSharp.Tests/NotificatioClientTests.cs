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
		private string NumberToSendMessage { get; set; }

		private string NumberToAuthenticate { get; set; }

		[TestInitialize]
		public void Init()
		{
			// Setup your phone numbers in app.config
			NumberToAuthenticate = ConfigurationManager.AppSettings["numberToAuthenticate"];
			if (string.IsNullOrEmpty(NumberToAuthenticate))
				throw new InvalidOperationException("NumberToAuthenticate is null. Specify number in app.config");

			NumberToSendMessage = ConfigurationManager.AppSettings["numberToSendMessage"];
			if (string.IsNullOrEmpty(NumberToSendMessage))
				throw new InvalidOperationException("NumberToSendMessage is null. Specify number in app.config");
		}

		[TestMethod]
		public async Task AuthUser()
		{
			var store = new FileSessionStore();
			var client = new TelegramClient(store, "session");

			await client.Connect();

			var hash = await client.SendCodeRequest(NumberToAuthenticate);
			var code = "86474"; // you can change code in debugger

			var user = await client.MakeAuth(NumberToAuthenticate, hash, code);

			Assert.IsNotNull(user);
		}

		[TestMethod]
		public async Task CheckPhones()
		{
			var store = new FileSessionStore();
			var client = new TelegramClient(store, "session");
			await client.Connect();

			var result = await client.IsPhoneRegistered(NumberToAuthenticate);
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task ImportContact()
		{
			// User should be already authenticated!

			var store = new FileSessionStore();
			var client = new TelegramClient(store, "session");
			
			await client.Connect();

			Assert.IsTrue(client.IsUserAuthorized());

			var res = await client.ImportContact(NumberToSendMessage);

			Assert.IsNotNull(res);
		}

		[TestMethod]
		public async Task SendMessage()
		{
			// User should be already authenticated!

			var store = new FileSessionStore();
			var client = new TelegramClient(store, "session");
			await client.Connect();

			Assert.IsTrue(client.IsUserAuthorized());

			var res = await client.ImportContact(NumberToSendMessage);

			Assert.IsNotNull(res);

			await client.SendMessage(res.Value, "Test message from TelegramClient");
		}

		[TestMethod]
		public async Task TestConnection()
		{
			var store = new FakeSessionStore();
			var client = new TelegramClient(store, "");

			Assert.IsTrue(await client.Connect());
		}

		[TestMethod]
		public async Task AuthenticationWorks()
		{
			using (var transport = new TcpTransport("91.108.56.165", 443))
			{
				var authKey = await Authenticator.DoAuthentication(transport);

				Assert.IsNotNull(authKey.AuthKey.Data);
			}
		}
	}
}
