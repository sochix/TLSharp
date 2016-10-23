using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeleSharp.TL;
using TeleSharp.TL.Channels;
using TeleSharp.TL.Contacts;
using TeleSharp.TL.Messages;
using TeleSharp.TL.Upload;
using TLSharp.Core;
using TLSharp.Core.Auth;
using TLSharp.Core.MTProto;
using TLSharp.Core.Network;
using TLSharp.Core.Requests;
using TLSharp.Core.Utils;

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

        private string apiHash = null;

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
            var client = new TelegramClient(apiId, apiHash);

            await client.ConnectAsync();

            var hash = await client.SendCodeRequestAsync(NumberToAuthenticate);
            var code = "93463"; // you can change code in debugger

            var user = await client.MakeAuthAsync(NumberToAuthenticate, hash, code);

            Assert.IsNotNull(user);
            Assert.IsTrue(client.IsUserAuthorized());
        }

        [TestMethod]
        public async Task SendMessageTest()
        {
            var client = new TelegramClient(apiId, apiHash);

            await client.ConnectAsync();

            var result = await client.GetContactsAsync();

            var user = result.users.lists
                .Where(x => x.GetType() == typeof(TLUser))
                .Cast<TLUser>()
                .FirstOrDefault(x => x.phone == NumberToSendMessage);

            if (user == null)
            {
                throw new System.Exception("Number was not found in Contacts List of user: " + NumberToSendMessage);
            }

            await client.SendTypingAsync(new TLInputPeerUser() { user_id = user.id });
            Thread.Sleep(3000);
            await client.SendMessageAsync(new TLInputPeerUser() { user_id = user.id }, "TEST");

        }

        [TestMethod]
        public async Task SendMessageToChannelTest()
        {
            var client = new TelegramClient(apiId, apiHash);

            await client.ConnectAsync();

            var dialogs = await client.GetUserDialogsAsync();
            var chat = dialogs.chats.lists
                .Where(c => c.GetType() == typeof(TLChannel))
                .Cast<TLChannel>()
                .FirstOrDefault(c => c.title == "TestGroup");

            await client.SendMessageAsync(new TLInputPeerChannel() { channel_id = chat.id, access_hash = chat.access_hash.Value }, "TEST MSG");
        }

        [TestMethod]
        public async Task SendPhotoToContactTest()
        {
            var client = new TelegramClient(apiId, apiHash);

            await client.ConnectAsync();

            var result = await client.GetContactsAsync();

            var user = result.users.lists
                .Where(x => x.GetType() == typeof(TLUser))
                .Cast<TLUser>()
                .FirstOrDefault(x => x.phone == NumberToSendMessage);

            var fileResult = (TLInputFile)await client.UploadFile("cat.jpg", new StreamReader("data/cat.jpg"));
            await client.SendUploadedPhoto(new TLInputPeerUser() { user_id = user.id }, fileResult, "kitty");
        }

        [TestMethod]
        public async Task SendBigFileToContactTest()
        {
            var client = new TelegramClient(apiId, apiHash);

            await client.ConnectAsync();

            var result = await client.GetContactsAsync();

            var user = result.users.lists
                .Where(x => x.GetType() == typeof(TLUser))
                .Cast<TLUser>()
                .FirstOrDefault(x => x.phone == NumberToSendMessage);

            var fileResult = (TLInputFileBig)await client.UploadFile("some.zip", new StreamReader("<some big file path>"));

            await client.SendUploadedDocument(
                new TLInputPeerUser() { user_id = user.id },
                fileResult,
                "some zips",
                "application/zip",
                new TLVector<TLAbsDocumentAttribute>());
        }

        [TestMethod]
        public async Task DownloadFileFromContactTest()
        {
            var client = new TelegramClient(apiId, apiHash);

            await client.ConnectAsync();

            var result = await client.GetContactsAsync();

            var user = result.users.lists
                .Where(x => x.GetType() == typeof(TLUser))
                .Cast<TLUser>()
                .FirstOrDefault(x => x.phone == NumberToSendMessage);

            var inputPeer = new TLInputPeerUser() { user_id = user.id };
            var res = await client.SendRequestAsync<TLMessagesSlice>(new TLRequestGetHistory() { peer = inputPeer });
            var document = res.messages.lists
                .Where(m => m.GetType() == typeof(TLMessage))
                .Cast<TLMessage>()
                .Where(m => m.media != null && m.media.GetType() == typeof(TLMessageMediaDocument))
                .Select(m => m.media)
                .Cast<TLMessageMediaDocument>()
                .Where(md => md.document.GetType() == typeof(TLDocument))
                .Select(md => md.document)
                .Cast<TLDocument>()
                .First();

            var resFile = await client.GetFile(
                new TLInputDocumentFileLocation()
                {
                    access_hash = document.access_hash,
                    id = document.id,
                    version = document.version
                },
                document.size);
            
            Assert.IsTrue(resFile.bytes.Length > 0);
        }

        [TestMethod]
        public async Task SignUpNewUser()
        {
            var client = new TelegramClient(apiId, apiHash);
            await client.ConnectAsync();

            var hash = await client.SendCodeRequestAsync(NotRegisteredNumberToSignUp);
            var code = "";

            var registeredUser = await client.SignUpAsync(NotRegisteredNumberToSignUp, hash, code, "TLSharp", "User");
            Assert.IsNotNull(registeredUser);
            Assert.IsTrue(client.IsUserAuthorized());

            var loggedInUser = await client.MakeAuthAsync(NotRegisteredNumberToSignUp, hash, code);
            Assert.IsNotNull(loggedInUser);
        }

        [TestMethod]
        public async Task CheckPhones()
        {
            var client = new TelegramClient(apiId, apiHash);
            await client.ConnectAsync();

            var result = await client.IsPhoneRegisteredAsync(NumberToAuthenticate);
            Assert.IsTrue(result);
        }
    }
}
