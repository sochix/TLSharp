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

        [TestMethod]
        public async Task SignUpNewUser()
        {
            var store = new FileSessionStore();
            var client = new TelegramClient(store, "session", apiId, apiHash);
            await client.Connect();

            var hash = await client.SendCodeRequest(NotRegisteredNumberToSignUp);
            var code = "";

            var registeredUser = await client.SignUp(NotRegisteredNumberToSignUp, hash, code, "TLSharp", "User");
            Assert.IsNotNull(registeredUser);
            Assert.IsTrue(client.IsUserAuthorized());

            var loggedInUser = await client.MakeAuth(NotRegisteredNumberToSignUp, hash, code);
            Assert.IsNotNull(loggedInUser);
        }

        [TestMethod]
        public async Task CheckPhones()
        {
            var store = new FileSessionStore();
            var client = new TelegramClient(store, "session", apiId, apiHash);
            await client.Connect();

            var result = await client.IsPhoneRegistered(NumberToAuthenticate);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task ImportContactByPhoneNumber()
        {
            // User should be already authenticated!

            var store = new FileSessionStore();
            var client = new TelegramClient(store, "session", apiId, apiHash);

            await client.Connect();

            Assert.IsTrue(client.IsUserAuthorized());

            var res = await client.ImportContactByPhoneNumber(NumberToSendMessage);

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public async Task ImportByUserName()
        {
            var store = new FileSessionStore();
            var client = new TelegramClient(store, "session", apiId, apiHash);

            await client.Connect();

            Assert.IsTrue(client.IsUserAuthorized());

            var res = await client.ImportByUserName(UserNameToSendMessage);

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public async Task ImportByUserNameAndSendMessage()
        {
            var store = new FileSessionStore();
            var client = new TelegramClient(store, "session", apiId, apiHash);

            await client.Connect();

            Assert.IsTrue(client.IsUserAuthorized());

            var res = await client.ImportByUserName(UserNameToSendMessage);

            Assert.IsNotNull(res);

            await client.SendMessage(res.Value, "Test message from TelegramClient");
        }

        [TestMethod]
        public async Task ImportContactByPhoneNumberAndSendMessage()
        {
            // User should be already authenticated!

            var store = new FileSessionStore();
            var client = new TelegramClient(store, "session", apiId, apiHash);
            await client.Connect();

            Assert.IsTrue(client.IsUserAuthorized());

            var res = await client.ImportContactByPhoneNumber(NumberToSendMessage);

            Assert.IsNotNull(res);

            await client.SendMessage(res.Value, "Test message from TelegramClient");
        }

        [TestMethod]
        public async Task GetHistory()
        {
            var store = new FileSessionStore();
            var client = new TelegramClient(store, "session", apiId, apiHash);
            await client.Connect();

            Assert.IsTrue(client.IsUserAuthorized());

            var res = await client.ImportContactByPhoneNumber(NumberToSendMessage);

            Assert.IsNotNull(res);

            var hist = await client.GetMessagesHistoryForContact(res.Value, 0, 5);

            Assert.IsNotNull(hist);
        }

        [TestMethod]
        public async Task UploadAndSendMedia()
        {
            var store = new FileSessionStore();
            var client = new TelegramClient(store, "session", apiId, apiHash);
            await client.Connect();

            Assert.IsTrue(client.IsUserAuthorized());

            var res = await client.ImportContactByPhoneNumber(NumberToSendMessage);

            Assert.IsNotNull(res);

            var file = File.ReadAllBytes("../../data/cat.jpg");

            var mediaFile = await client.UploadFile("test_file.jpg", file);

            Assert.IsNotNull(mediaFile);

            var state = await client.SendMediaMessage(res.Value, mediaFile);

            Assert.IsTrue(state);
        }

        [TestMethod]
        public async Task GetFile()
        {
            // Get uploaded file from last message (ie: cat.jpg)

            var store = new FileSessionStore();
            var client = new TelegramClient(store, "session", apiId, apiHash);
            await client.Connect();
            Assert.IsTrue(client.IsUserAuthorized());

            var res = await client.ImportContactByPhoneNumber(NumberToSendMessage);
            Assert.IsNotNull(res);

            // Get last message
            var hist = await client.GetMessagesHistoryForContact(res.Value, 0, 1);
            Assert.AreEqual(1, hist.Count);

            var message = (MessageConstructor) hist[0];
            Assert.AreEqual(typeof (MessageMediaPhotoConstructor), message.media.GetType());

            var media = (MessageMediaPhotoConstructor) message.media;
            Assert.AreEqual(typeof (PhotoConstructor), media.photo.GetType());

            var photo = (PhotoConstructor) media.photo;
            Assert.AreEqual(3, photo.sizes.Count);
            Assert.AreEqual(typeof (PhotoSizeConstructor), photo.sizes[2].GetType());

            var photoSize = (PhotoSizeConstructor) photo.sizes[2];
            Assert.AreEqual(typeof (FileLocationConstructor), photoSize.location.GetType());

            var fileLocation = (FileLocationConstructor) photoSize.location;
            var file = await client.GetFile(fileLocation.volume_id, fileLocation.local_id, fileLocation.secret, 0, photoSize.size + 1024);
            storage_FileType type = file.Item1;
            byte[] bytes = file.Item2;

            string name = "../../data/get_file.";
            if (type.GetType() == typeof (Storage_fileJpegConstructor))
                name += "jpg";
            else if (type.GetType() == typeof (Storage_fileGifConstructor))
                name += "gif";
            else if (type.GetType() == typeof (Storage_filePngConstructor))
                name += "png";

            using (var fileStream = new FileStream(name, FileMode.Create, FileAccess.Write))
            {
                fileStream.Write(bytes, 4, photoSize.size); // The first 4 bytes seem to be the error code
            }
        }

        [TestMethod]
        public async Task TestConnection()
        {
            var store = new FakeSessionStore();
            var client = new TelegramClient(store, "", apiId, apiHash);

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

        [TestMethod]
        public async Task GetUserFullRequest()
        {
            var store = new FileSessionStore();
            var client = new TelegramClient(store, "session", apiId, apiHash);
            await client.Connect();

            Assert.IsTrue(client.IsUserAuthorized());

            var res = await client.ImportContactByPhoneNumber(NumberToGetUserFull);

            Assert.IsNotNull(res);

            var userFull = await client.GetUserFull(res.Value);

            Assert.IsNotNull(userFull);
        }

        [TestMethod]
        public async Task CreateChatRequest()
        {
            var client = await InitializeClient();

            var chatName = Guid.NewGuid().ToString();
            var statedMessage = await client.CreateChat(chatName, new List<string> {NumberToSendMessage});

            var createdChat = GetChatFromStatedMessage(statedMessage);

            Assert.AreEqual(chatName, createdChat.title);
            Assert.AreEqual(2, createdChat.participants_count);
        }

        [TestMethod]
        public async Task AddChatUserRequest()
        {
            var client = await InitializeClient();

            var chatName = Guid.NewGuid().ToString();
            var statedMessageAfterCreation = await client.CreateChat(chatName, new List<string> { NumberToSendMessage });

            var createdChat = GetChatFromStatedMessage(statedMessageAfterCreation);

            var addUserId = await client.ImportContactByPhoneNumber("380685004559");

            var statedMessageAfterAddUser = await client.AddChatUser(createdChat.id, addUserId.Value);
            var modifiedChat = GetChatFromStatedMessage(statedMessageAfterAddUser);

            Assert.AreEqual(createdChat.id, modifiedChat.id);
            Assert.AreEqual(3, modifiedChat.participants_count);
        }

        [TestMethod]
        public async Task LeaveChatRequest()
        {
            var client = await InitializeClient();

            var chatName = Guid.NewGuid().ToString();
            var statedMessageAfterCreation = await client.CreateChat(chatName, new List<string> { NumberToSendMessage });

            var createdChat = GetChatFromStatedMessage(statedMessageAfterCreation);
            
            var statedMessageAfterLeave = await client.LeaveChat(createdChat.id);
            var modifiedChat = GetChatFromStatedMessage(statedMessageAfterLeave);

            Assert.AreEqual(createdChat.id, modifiedChat.id);
            Assert.AreEqual(1, modifiedChat.participants_count);
        }

        private ChatConstructor GetChatFromStatedMessage(Messages_statedMessageConstructor message)
        {
            var serviceMessage = message.message as MessageServiceConstructor;
            var peerChat = serviceMessage.to_id as PeerChatConstructor;

            var createdChatId = peerChat.chat_id;
            return message.chats.OfType<ChatConstructor>().Single(c => c.id == createdChatId);
        }

        private async Task<TelegramClient> InitializeClient()
        {
            var store = new FileSessionStore();
            var client = new TelegramClient(store, "session", apiId, apiHash);
            await client.Connect();

            if (!client.IsUserAuthorized())
            {
                var hash = await client.SendCodeRequest(NumberToAuthenticate);

                var code = ""; // you can change code in debugger
                Debugger.Break();

                await client.MakeAuth(NumberToAuthenticate, hash, code);
            }

            Assert.IsTrue(client.IsUserAuthorized());

            return client;
        }
    }
}
