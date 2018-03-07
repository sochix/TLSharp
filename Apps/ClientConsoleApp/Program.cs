using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeleSharp.TL;
using TLSharp.Core;
using TLSharp.Core.MTProto.Crypto;
using static TLSharp.Core.MTProto.Serializers;

namespace ClientConsoleApp
{
    class Program
    {
        private static string NumberToSendMessage => ConfigurationManager.AppSettings[nameof(NumberToSendMessage)];

        static void Main(string[] args)
        {
            //TestRSA();
            Thread.Sleep(2000);
            Console.WriteLine("Hello World!");

            //var tcpClient = new TcpClient();
            //tcpClient.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000));
            //TestTcpClient(tcpClient);
            //TestTcpClient(tcpClient);

            TelegramClient client = GetTlgClient().Result;
            var normalizedNumber = NumberToSendMessage.StartsWith("+") ?
                NumberToSendMessage.Substring(1, NumberToSendMessage.Length - 1) :
                NumberToSendMessage;

            var result = client.GetContactsAsync().Result;

            var user = result.Users
                .OfType<TLUser>()
                .FirstOrDefault(x => x.Phone == normalizedNumber);

            var rr = client.SendTypingAsync(new TLInputPeerUser() { UserId = user.Id }).Result;
            Thread.Sleep(3000);
            var rrr = client.SendMessageAsync(new TLInputPeerUser() { UserId = user.Id }, "TEST").Result;
        }
        private static async Task<TLSharp.Core.TelegramClient> GetTlgClient()
        {
            string ApiHash = ConfigurationManager.AppSettings["ApiHash"];
            int ApiId = int.Parse(ConfigurationManager.AppSettings["ApiId"]);
            //string AuthCode = ConfigurationManager.AppSettings["AuthCode"];
            string MainPhoneNumber = ConfigurationManager.AppSettings["NumberToAuthenticate"];
            var client = new TelegramClient(ApiId, ApiHash, null, "session", null, "127.0.0.1", 5000);//
            var phoneCodeHash = string.Empty;
            authenticated:
            Console.WriteLine("Start app");
            client.ConnectAsync().Wait();
            Console.WriteLine("MakeAuthentication");
            await MakeAuthentication(client, MainPhoneNumber);

            Console.WriteLine("Connected");
            if (!client.IsUserAuthorized())
            {
                Console.WriteLine("User not autorized");
                if (string.IsNullOrEmpty(phoneCodeHash))
                {
                    phoneCodeHash = await client.SendCodeRequestAsync(MainPhoneNumber);
                    goto authenticated;
                }

                Console.WriteLine("Plase enter new AuthCode:");
                var AuthCode = Console.ReadLine();
                var user = await client.MakeAuthAsync(MainPhoneNumber, phoneCodeHash, AuthCode);
                Console.WriteLine("Login successfull.");
            }

            return client;
        }

        private static async Task MakeAuthentication(TLSharp.Core.TelegramClient client, string mainPhoneNumber)
        {
            var hash = await client.SendCodeRequestAsync(mainPhoneNumber);
            Console.WriteLine("waiting for code");
            var code = Console.ReadLine();
            var user = await client.MakeAuthAsync(mainPhoneNumber, hash, code);
            if (!client.IsUserAuthorized())
            {
                hash = await client.SendCodeRequestAsync(mainPhoneNumber);
                Console.WriteLine("please try again.add code");
                code = Console.ReadLine();
                user = await client.MakeAuthAsync(mainPhoneNumber, hash, code);
            }

        }

        private static void TestTcpClient(TcpClient tcpClient)
        {
            if (tcpClient.Connected)
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var binaryWriter = new BinaryWriter(memoryStream))
                    {
                        binaryWriter.Write((long)0);
                        binaryWriter.Write(1234);
                        binaryWriter.Write(20);
                        binaryWriter.Write(8000);

                        byte[] packet = memoryStream.ToArray();

                        tcpClient.GetStream().WriteAsync(packet, 0, packet.Length).Wait();
                    }
                }
            }

            Thread.Sleep(5000);
        }

        private static void TestNewNonce()
        {
            var g = 47;
            BigInteger a = new BigInteger(2048, new Random());
            BigInteger b = new BigInteger(2048, new Random());
            var dhPrime = new BigInteger("20030004000", 16);

            var ga = BigInteger.ValueOf(g).ModPow(a, dhPrime);
            var gb = BigInteger.ValueOf(g).ModPow(b, dhPrime);

            var ka = gb.ModPow(a, dhPrime);
            var kb = ga.ModPow(b, dhPrime);


            if (ka.Equals(kb))
            {

            }

            return;
        }

        private static void TestRSA()
        {
            BigInteger e, n, d;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    var keys = rsa.ExportParameters(true);

                    var strE = BitConverter.ToString(keys.Exponent).Replace("-", "");
                    var strN = BitConverter.ToString(keys.Modulus).Replace("-", "");
                    var strD = BitConverter.ToString(keys.D).Replace("-", "");

                    e = new BigInteger(strE, 16);
                    n = new BigInteger(strN, 16);
                    d = new BigInteger(strD, 16);

                    //e = new BigInteger(1, keys.Exponent);
                    //n = new BigInteger(1, keys.Modulus);
                    //d = new BigInteger(1, keys.D);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
           
            var text1 = "abcd--------------------------------123456789--------------------------------------------------------------------------------mnabcd--------------------------------123456789-----------------------------------------------------------------------------mn";
            var data = Encoding.ASCII.GetBytes(text1);

            byte[] ciphertext = new BigInteger(1, data).ModPow(e, n).ToByteArrayUnsigned();
            byte[] cleartext = new BigInteger(1, ciphertext).ModPow(d, n).ToByteArrayUnsigned();

            var text2 = ASCIIEncoding.ASCII.GetString(cleartext);

            return;
        }
    }
}
