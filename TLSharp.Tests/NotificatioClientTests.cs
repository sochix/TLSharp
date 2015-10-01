using System;
using System.Configuration;
using System.Threading;
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
		public string NumberToAuthenticate { get; set; }
		public string RegisteredNumber { get; set; }
		public string UnregisteredNumber { get; set; }

		[TestInitialize]
		public void Init()
		{
			// Setup your phone numbers in app.config
			NumberToAuthenticate = ConfigurationManager.AppSettings["numberToAuthenticate"];
			if (string.IsNullOrEmpty(NumberToAuthenticate))
				throw new InvalidOperationException("NumberToAuthenticate is null");

			RegisteredNumber = ConfigurationManager.AppSettings["registeredNumber"];
			if (string.IsNullOrEmpty(RegisteredNumber))
				throw new InvalidOperationException("RegisteredNumber is null");

			UnregisteredNumber = ConfigurationManager.AppSettings["unregisteredNumber"];
			if (string.IsNullOrEmpty(UnregisteredNumber))
				throw new InvalidOperationException("UnregisteredNumber is null");

			if (NumberToAuthenticate == UnregisteredNumber)
				throw new InvalidOperationException("NumberToAuthenticate eqauls UnregisteredNumber but shouldn't!");

			if (RegisteredNumber == UnregisteredNumber)
				throw new InvalidOperationException("RegisteredNumber eqauls UnregisteredNumber but shouldn't!");
		}

		[TestMethod]
		public async Task AuthUser()
		{
			var store = new FileSessionStore();
			var client = new TelegramClient(store, "session");

			await client.Connect();

			var hash = await client.SendCodeRequest(NumberToAuthenticate);
			var code = "123"; // you can change code in debugger

			var user = await client.MakeAuth(NumberToAuthenticate, hash, code);

			Assert.IsNotNull(user);
		}

		[TestMethod]
		public async Task CheckPhones()
		{
			var store = new FileSessionStore();
			var client = new Core.TelegramClient(store, "session");
			await client.Connect();

			var phoneList = new string[]
			{
				RegisteredNumber,
				NumberToAuthenticate
			};

			var rand = new Random();
			foreach (var phone in phoneList)
			{
				var result = await client.IsPhoneRegistered(phone);
				Thread.Sleep(rand.Next(9) * 1000);
				if (result)
					Console.WriteLine($"{phone} - OK");
			}
		}

		[TestMethod]
		public async Task ImportContact()
		{
			// User should be already authenticated!

			var store = new FileSessionStore();
			var client = new Core.TelegramClient(store, "session");
			
			await client.Connect();

			Assert.IsTrue(client.IsUserAuthorized());

			var res = await client.ImportContact(RegisteredNumber);

			Assert.IsNotNull(res);

		}

		[TestMethod]
		public async Task SendMessage()
		{
			// User should be already authenticated!

			var store = new FileSessionStore();
			var client = new Core.TelegramClient(store, "session");
			await client.Connect();

			Assert.IsTrue(client.IsUserAuthorized());

			var res = await client.ImportContact(RegisteredNumber);

			Assert.IsNotNull(res);

			await client.SendMessage(res.Value, "Test message from TelegramClient");
		}

		[TestMethod]
		public async Task TestConnection()
		{
			var store = new FakeSessionStore();
			var client = new Core.TelegramClient(store, "");

			Assert.IsTrue(await client.Connect());
		}

		[TestMethod]
		public async Task AuthenticationWorks()
		{
			using (var transport = new TcpTransport())
			{
				var authKey = await Authenticator.DoAuthentication(transport);

				Assert.IsNotNull(authKey.AuthKey.Data);
			}
		}
	}
}
