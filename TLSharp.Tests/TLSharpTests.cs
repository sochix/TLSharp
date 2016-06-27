using System;
using System.Configuration;
using System.IO;
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

        private string UserNameToSendMessage { get; set; }

        private string NumberToGetUserFull { get; set; }

        private string apiHash = "";

        private int apiId = 0;

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

            UserNameToSendMessage = ConfigurationManager.AppSettings["userNameToSendMessage"];
            if (string.IsNullOrEmpty(UserNameToSendMessage))
                throw new InvalidOperationException("UserNameToSendMessage is null. Specify userName in app.config");

            NumberToGetUserFull = ConfigurationManager.AppSettings["numberToGetUserFull"];
            if (string.IsNullOrEmpty(NumberToGetUserFull))
                throw new InvalidOperationException("NumberToGetUserFull is null. Specify Number in app.config");

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
            Assert.AreEqual(typeof(MessageMediaPhotoConstructor), message.media.GetType());

            var media = (MessageMediaPhotoConstructor) message.media;
            Assert.AreEqual(typeof(PhotoConstructor), media.photo.GetType());

            var photo = (PhotoConstructor) media.photo;
            Assert.AreEqual(3, photo.sizes.Count);
            Assert.AreEqual(typeof(PhotoSizeConstructor), photo.sizes[2].GetType());

            var photoSize = (PhotoSizeConstructor) photo.sizes[2];
            Assert.AreEqual(typeof(FileLocationConstructor), photoSize.location.GetType());

            var fileLocation = (FileLocationConstructor) photoSize.location;
            var file = await client.GetFile(fileLocation.volume_id, fileLocation.local_id, fileLocation.secret, 0, photoSize.size + 1024);
            storage_FileType type = file.Item1;
            byte[] bytes = file.Item2;

            string name = "../../data/get_file.";
            if (type.GetType() == typeof(Storage_fileJpegConstructor))
                name += "jpg";
            else if (type.GetType() == typeof(Storage_fileGifConstructor))
                name += "gif";
            else if (type.GetType() == typeof(Storage_filePngConstructor))
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
    }
}
