using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TLSharp.Core;
using TLSharp.Core.Auth;
using TLSharp.Core.MTProto;
using TLSharp.Core.Network;

namespace TLSharp.Tests
{
    [TestClass]
    public class TLSharpTests
    {
        private string NumberToSendMessage { get; set; }

        private string NumberToAuthenticate { get; set; }

        private string NotRegisteredNumberToSignUp { get; set; }

        private string UserNameToSendMessage { get; set; }

        private string NumberToGetUserFull { get; set; }

        private string NumberToAddToChat { get; set; }

        private string apiHash = "";

        private int apiId = 0;

        [TestInitialize]
        public void Init()
        {
            // Setup your phone numbers in app.config
            NumberToAuthenticate = ConfigurationManager.AppSettings[nameof(NumberToAuthenticate)];
            if (string.IsNullOrEmpty(NumberToAuthenticate))
                Debug.WriteLine("NumberToAuthenticate not configured in app.config! Some tests may fail.");

            NotRegisteredNumberToSignUp = ConfigurationManager.AppSettings[nameof(NotRegisteredNumberToSignUp)];
            if (string.IsNullOrEmpty(NotRegisteredNumberToSignUp))
                Debug.WriteLine("NotRegisteredNumberToSignUp not configured in app.config! Some tests may fail.");

            NumberToSendMessage = ConfigurationManager.AppSettings[nameof(NumberToSendMessage)];
            if (string.IsNullOrEmpty(NumberToSendMessage))
                Debug.WriteLine("NumberToSendMessage not configured in app.config! Some tests may fail.");

            UserNameToSendMessage = ConfigurationManager.AppSettings[nameof(UserNameToSendMessage)];
            if (string.IsNullOrEmpty(UserNameToSendMessage))
                Debug.WriteLine("UserNameToSendMessage not configured in app.config! Some tests may fail.");

            NumberToGetUserFull = ConfigurationManager.AppSettings[nameof(NumberToGetUserFull)];
            if (string.IsNullOrEmpty(NumberToGetUserFull))
                Debug.WriteLine("NumberToGetUserFull not configured in app.config! Some tests may fail.");

            NumberToAddToChat = ConfigurationManager.AppSettings[nameof(NumberToAddToChat)];
            if (string.IsNullOrEmpty(NumberToAddToChat))
                Debug.WriteLine("NumberToAddToChat not configured in app.config! Some tests may fail.");
        }

        [TestMethod]
        public async Task AuthUser()
        {
            var store = new FileSessionStore();
            var client = new TelegramClient(store, "session", apiId, apiHash);

            await client.Connect();

            var hash = await client.SendCodeRequest(NumberToAuthenticate);
            var code = "93463"; // you can change code in debugger

            var user = await client.MakeAuth(NumberToAuthenticate, hash, code);

            Assert.IsNotNull(user);
            Assert.IsTrue(client.IsUserAuthorized());
        }
    }
}
