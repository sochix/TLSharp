
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using TeleSharp.TL;
using TeleSharp.TL.Messages;
using TLSharp.Core;
using TLSharp.Core.Requests;
using TLSharp.Core.Utils;

using Test = NUnit.Framework.TestAttribute;
using TestFixture = NUnit.Framework.TestFixtureAttribute;
using TestFixtureSetUp = NUnit.Framework.TestFixtureSetUpAttribute;

namespace TLSharp.Tests
{
    [TestFixture]
    public class TLSharpTests
    {
        private string NumberToSendMessage { get; set; }

        private string NumberToAuthenticate { get; set; }

        private string CodeToAuthenticate { get; set; }

        private string NotRegisteredNumberToSignUp { get; set; }

        private string UserNameToSendMessage { get; set; }

        private string NumberToGetUserFull { get; set; }

        private string NumberToAddToChat { get; set; }

        private string ApiHash { get; set; }

        private int ApiId { get; set; }

        [TestFixtureSetUp]
        public virtual void Init()
        {
            Init(o => NUnit.Framework.Assert.IsNotNull(o), b => NUnit.Framework.Assert.IsTrue(b));
        }

        private TelegramClient NewClient()
        {
            try
            {
                return new TelegramClient(ApiId, ApiHash);
            }
            catch (MissingApiConfigurationException ex)
            {
                throw new Exception($"Please add your API settings to the `app.config` file. (More info: {MissingApiConfigurationException.InfoUrl})",
                                    ex);
            }
        }

        private void GatherTestConfiguration()
        {
            string appConfigMsgWarning = "{0} not configured in app.config! Some tests may fail.";

            ApiHash = ConfigurationManager.AppSettings[nameof(ApiHash)];
            if (string.IsNullOrEmpty(ApiHash))
                Debug.WriteLine(appConfigMsgWarning, nameof(ApiHash));

            var apiId = ConfigurationManager.AppSettings[nameof(ApiId)];
            if (string.IsNullOrEmpty(apiId))
                Debug.WriteLine(appConfigMsgWarning, nameof(ApiId));
            else
                ApiId = int.Parse(apiId);

            NumberToAuthenticate = ConfigurationManager.AppSettings[nameof(NumberToAuthenticate)];
            if (string.IsNullOrEmpty(NumberToAuthenticate))
                Debug.WriteLine(appConfigMsgWarning, nameof(NumberToAuthenticate));

            CodeToAuthenticate = ConfigurationManager.AppSettings[nameof(CodeToAuthenticate)];
            if (string.IsNullOrEmpty(CodeToAuthenticate))
                Debug.WriteLine(appConfigMsgWarning, nameof(CodeToAuthenticate));

            NotRegisteredNumberToSignUp = ConfigurationManager.AppSettings[nameof(NotRegisteredNumberToSignUp)];
            if (string.IsNullOrEmpty(NotRegisteredNumberToSignUp))
                Debug.WriteLine(appConfigMsgWarning, nameof(NotRegisteredNumberToSignUp));

            UserNameToSendMessage = ConfigurationManager.AppSettings[nameof(UserNameToSendMessage)];
            if (string.IsNullOrEmpty(UserNameToSendMessage))
                Debug.WriteLine(appConfigMsgWarning, nameof(UserNameToSendMessage));

            NumberToGetUserFull = ConfigurationManager.AppSettings[nameof(NumberToGetUserFull)];
            if (string.IsNullOrEmpty(NumberToGetUserFull))
                Debug.WriteLine(appConfigMsgWarning, nameof(NumberToGetUserFull));

            NumberToAddToChat = ConfigurationManager.AppSettings[nameof(NumberToAddToChat)];
            if (string.IsNullOrEmpty(NumberToAddToChat))
                Debug.WriteLine(appConfigMsgWarning, nameof(NumberToAddToChat));
        }

        [Test]
        public virtual async Task AuthUser()
        {
            var client = NewClient();

            await client.ConnectAsync();

            var hash = await client.SendCodeRequestAsync(NumberToAuthenticate);
            var code = CodeToAuthenticate; // you can change code in debugger too

            if (String.IsNullOrWhiteSpace(code))
            {
                throw new Exception("CodeToAuthenticate is empty in the app.config file, fill it with the code you just got now by SMS/Telegram");
            }

            TLUser user = null;
            try
            {
                user = await client.MakeAuthAsync(NumberToAuthenticate, hash, code);
            }
            catch (InvalidPhoneCodeException ex)
            {
                throw new Exception("CodeToAuthenticate is wrong in the app.config file, fill it with the code you just got now by SMS/Telegram",
                                    ex);
            }

            Assert.IsNotNull(user);
            Assert.IsTrue(client.IsUserAuthorized());
        }

        [Test]
        public virtual async Task SendMessageTest()
        {
            NumberToSendMessage = ConfigurationManager.AppSettings[nameof(NumberToSendMessage)];
            if (string.IsNullOrWhiteSpace(NumberToSendMessage))
                throw new Exception($"Please fill the '{nameof(NumberToSendMessage)}' setting in app.config file first");

            // this is because the contacts in the address come without the "+" prefix
            var normalizedNumber = NumberToSendMessage.StartsWith("+") ?
                NumberToSendMessage.Substring(1, NumberToSendMessage.Length - 1) :
                NumberToSendMessage;

            var client = NewClient();

            await client.ConnectAsync();

            var result = await client.GetContactsAsync();

            var user = result.users.lists
                .Where(x => x.GetType() == typeof(TLUser))
                .Cast<TLUser>()
                .FirstOrDefault(x => x.phone == normalizedNumber);

            if (user == null)
            {
                throw new System.Exception("Number was not found in Contacts List of user: " + NumberToSendMessage);
            }

            await client.SendTypingAsync(new TLInputPeerUser() { user_id = user.id });
            Thread.Sleep(3000);
            await client.SendMessageAsync(new TLInputPeerUser() { user_id = user.id }, "TEST");
        }

        [Test]
        public virtual async Task SendMessageToChannelTest()
        {
            var client = NewClient();

            await client.ConnectAsync();

            var dialogs = (TLDialogs) await client.GetUserDialogsAsync();
            var chat = dialogs.chats.lists
                .Where(c => c.GetType() == typeof(TLChannel))
                .Cast<TLChannel>()
                .FirstOrDefault(c => c.title == "TestGroup");

            await client.SendMessageAsync(new TLInputPeerChannel() { channel_id = chat.id, access_hash = chat.access_hash.Value }, "TEST MSG");
        }

        [Test]
        public virtual async Task SendPhotoToContactTest()
        {
            var client = NewClient();

            await client.ConnectAsync();

            var result = await client.GetContactsAsync();

            var user = result.users.lists
                .Where(x => x.GetType() == typeof(TLUser))
                .Cast<TLUser>()
                .FirstOrDefault(x => x.phone == NumberToSendMessage);

            var fileResult = (TLInputFile)await client.UploadFile("cat.jpg", new StreamReader("data/cat.jpg"));
            await client.SendUploadedPhoto(new TLInputPeerUser() { user_id = user.id }, fileResult, "kitty");
        }

        [Test]
        public virtual async Task SendBigFileToContactTest()
        {
            var client = NewClient();

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

        [Test]
        public virtual async Task DownloadFileFromContactTest()
        {
            var client = NewClient();

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

        [Test]
        public virtual async Task DownloadFileFromWrongLocationTest()
        {
            var client = NewClient();

            await client.ConnectAsync();

            var result = await client.GetContactsAsync();

            var user = result.users.lists
                .Where(x => x.GetType() == typeof(TLUser))
                .Cast<TLUser>()
                .FirstOrDefault(x => x.id == 5880094);
    
            var photo = ((TLUserProfilePhoto)user.photo);
            var photoLocation = (TLFileLocation) photo.photo_big;

            var resFile = await client.GetFile(new TLInputFileLocation()
            {
                local_id = photoLocation.local_id,
                secret = photoLocation.secret,
                volume_id = photoLocation.volume_id
            }, 1024);

            var res = await client.GetUserDialogsAsync(); 

            Assert.IsTrue(resFile.bytes.Length > 0);
        }

        [Test]
        public virtual async Task SignUpNewUser()
        {
            var client = NewClient();
            await client.ConnectAsync();

            var hash = await client.SendCodeRequestAsync(NotRegisteredNumberToSignUp);
            var code = "";

            var registeredUser = await client.SignUpAsync(NotRegisteredNumberToSignUp, hash, code, "TLSharp", "User");
            Assert.IsNotNull(registeredUser);
            Assert.IsTrue(client.IsUserAuthorized());

            var loggedInUser = await client.MakeAuthAsync(NotRegisteredNumberToSignUp, hash, code);
            Assert.IsNotNull(loggedInUser);
        }

        [Test]
        public virtual async Task CheckPhones()
        {
            var client = NewClient();
            await client.ConnectAsync();

            var result = await client.IsPhoneRegisteredAsync(NumberToAuthenticate);
            Assert.IsTrue(result);
        }

        private static Action<object> IsNotNullHanlder;
        private static Action<bool> IsTrueHandler;

        protected void Init(Action<object> notNullHandler, Action<bool> trueHandler)
        {
            IsNotNullHanlder = notNullHandler;
            IsTrueHandler = trueHandler;

            // Setup your API settings and phone numbers in app.config
            GatherTestConfiguration();
        }

        class Assert
        {
            static internal void IsNotNull(object obj)
            {
                IsNotNullHanlder(obj);
            }

            static internal void IsTrue(bool cond)
            {
                IsTrueHandler(cond);
            }
        }
    }
}
