
using System;
using System.Threading.Tasks;

using NUnit.Framework;

namespace TgSharp.Tests
{
    [TestFixture]
    public class TgSharpTestsNUnit : TgSharpTests
    {
        [TestFixtureSetUp]
        public void Init()
        {
            base.Init(o => Assert.IsNotNull(o), b => Assert.IsTrue(b));
        }

        [Test]
        [Ignore("Only run again when you want a new CodeToAuthenticate value in your app.config")]
        public async override Task AuthUser()
        {
            await base.AuthUser();
        }

        [Test]
        public override async Task SendMessageTest()
        {
            await base.SendMessageTest();
        }

        [Test]
        [Ignore("Untested in CI")]
        public override async Task SendMessageToChannelTest()
        {
            await base.SendMessageToChannelTest();
        }

        [Test]
        public override async Task SendPhotoToContactTest()
        {
            await base.SendPhotoToContactTest();
        }

        [Test]
        [Ignore("Untested in CI")]
        public override async Task SendBigFileToContactTest()
        {
            await base.SendBigFileToContactTest();
        }

        [Test]
        [Ignore("Untested in CI")]
        public override async Task DownloadFileFromContactTest()
        {
            await base.DownloadFileFromContactTest();
        }

        [Test]
        [Ignore("Untested in CI")]
        public override async Task DownloadFileFromWrongLocationTest()
        {
            await base.DownloadFileFromWrongLocationTest();
        }

        [Test]
        [Ignore("Untested in CI")]
        public override async Task SignUpNewUser()
        {
            await base.SignUpNewUser();
        }

        [Test]
        public override async Task SendMessageByUserNameTest()
        {
            await base.SendMessageByUserNameTest();
        }
    }
}
