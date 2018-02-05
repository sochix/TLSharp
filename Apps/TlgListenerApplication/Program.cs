using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Ionic.Crc;
using TLSharp.Core.Auth;
using TLSharp.Core.MTProto.Crypto;
using TLSharp.Core.Network;
using TLSharp.Core.Utils;

namespace TlgListenerApplication
{
    class Program
    {
        private static int sequence = 0;
        private static int timeOffset;
        private static long lastMessageId;
        private static Random random => new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("Listening...");
            TcpListener();
            Console.WriteLine("The end");
            Console.ReadKey();
        }

        private static void TcpListener()
        {
            try
            {
                var tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 5000);
                tcpListener.Start();
                for (; ; )
                {
                    if (tcpListener.Pending())
                    {
                        try
                        {
                            ProcessRequest(tcpListener);
                        }
                        catch (Exception e)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(e);
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }


        private static void ProcessRequestBySocket(TcpListener tcpListener)
        {
            Console.WriteLine("Processing socket...");
            var socketClient = tcpListener.AcceptSocket();

            byte[] nonceFromClient = new byte[16];

            var bytes = new byte[socketClient.ReceiveBufferSize];
            socketClient.Receive(bytes);
            var tcpMessage = TcpMessage.Decode(bytes);
            var binaryReader = new BinaryReader(new MemoryStream(tcpMessage.Body, false));
            var a = binaryReader.ReadInt64();
            var msgId = binaryReader.ReadInt64();
            var datalength = binaryReader.ReadInt32();
            var data = binaryReader.ReadBytes(datalength);

            var binaryReader2 = new BinaryReader(new MemoryStream(data, false));
            const int responseConstructorNumber = 0x60469778;
            var responseCode = binaryReader2.ReadInt32();
            Console.WriteLine("Request code: " + responseCode);
            if (responseCode == responseConstructorNumber)//---Step1_PQRequest
            {
                nonceFromClient = binaryReader2.ReadBytes(16);
            }

            var nonce = new byte[16];
            new Random().NextBytes(nonce);

            var fingerprint = StringToByteArray("216be86c022bb4c3");

            var step1 = new Step1_Response()
            {
                Pq = new BigInteger(1, BitConverter.GetBytes(880)),
                ServerNonce = nonceFromClient,
                Nonce = nonce,
                Fingerprints = new List<byte[]>() { fingerprint }
            };
            var bytestosend = PrepareToSend(step1.ToBytes());
            var datatosend = Encode(bytestosend, 11);
            socketClient.Send(datatosend);
        }

        private static void ProcessRequest(TcpListener tcpListener)
        {
            Console.WriteLine("Processing...");
            var tcpClient = tcpListener.AcceptTcpClient();

            var netStream = tcpClient.GetStream();
            if (netStream.DataAvailable)
            {
                byte[] nonceFromClient = new byte[16];
                if (netStream.CanRead)
                {
                    var bytes = new byte[tcpClient.ReceiveBufferSize];
                    netStream.Read(bytes, 0, (int)tcpClient.ReceiveBufferSize);
                    var tcpMessage = TcpMessage.Decode(bytes);
                    var binaryReader = new BinaryReader(new MemoryStream(tcpMessage.Body, false));
                    //var a = binaryReader.ReadInt64();
                    var msgId = binaryReader.ReadInt64();
                    var datalength = binaryReader.ReadInt32();
                    var data = binaryReader.ReadBytes(datalength);

                    var binaryReader2 = new BinaryReader(new MemoryStream(data, false));
                    const int responseConstructorNumber = 0x60469778;
                    var responseCode = binaryReader2.ReadInt32();
                    Console.WriteLine("Request code: " + responseCode);
                    if (responseCode == responseConstructorNumber)//---Step1_PQRequest
                    {
                        nonceFromClient = binaryReader2.ReadBytes(16);
                    }

                    //var obj = new Step1_PQRequest().FromBytes(data);
                    //var rr = FromByteArray<Step1_PQRequest>(data);

                    //var binaryReader = new BinaryReader(netStream);
                    //var a = binaryReader.ReadInt64();
                    //var b = binaryReader.ReadInt32();
                    //var c = binaryReader.ReadInt32();
                    //var d = binaryReader.ReadInt32();


                    //Console.WriteLine("This is what the host returned to you: " + returndata);
                }

                if (netStream.CanWrite)
                {
                    var nonce = new byte[16];
                    new Random().NextBytes(nonce);

                    var fingerprint = StringToByteArray("216be86c022bb4c3");
                    //var rr = BitConverter.ToString(fingerprint).Replace("-", "");

                    var step1 = new Step1_Response()
                    {
                        Pq = new BigInteger(1, BitConverter.GetBytes(880)),
                        ServerNonce = nonceFromClient,
                        Nonce = nonce,
                        Fingerprints = new List<byte[]>() { fingerprint }
                    };
                    var bytes = PrepareToSend(step1.ToBytes());
                    var datatosend = Encode(bytes, 11);
                    //Byte[] sendBytes = Encoding.UTF8.GetBytes("Is anybody there?");
                    netStream.Write(datatosend, 0, datatosend.Length);
                }
                else
                {
                    Console.WriteLine("You cannot write data to this stream.");
                    netStream.Close();
                    tcpClient.Close();
                }
            }
        }

        public static async Task<TcpMessage> Receieve(TcpClient tcpClient)
        {
            var stream = tcpClient.GetStream();

            var packetLengthBytes = new byte[4];
            if (await stream.ReadAsync(packetLengthBytes, 0, 4) != 4)
                throw new InvalidOperationException("Couldn't read the packet length");
            int packetLength = BitConverter.ToInt32(packetLengthBytes, 0);

            var seqBytes = new byte[4];
            if (await stream.ReadAsync(seqBytes, 0, 4) != 4)
                throw new InvalidOperationException("Couldn't read the sequence");
            int seq = BitConverter.ToInt32(seqBytes, 0);

            int readBytes = 0;
            var body = new byte[packetLength - 12];
            int neededToRead = packetLength - 12;

            do
            {
                var bodyByte = new byte[packetLength - 12];
                var availableBytes = await stream.ReadAsync(bodyByte, 0, neededToRead);
                neededToRead -= availableBytes;
                Buffer.BlockCopy(bodyByte, 0, body, readBytes, availableBytes);
                readBytes += availableBytes;
            }
            while (readBytes != packetLength - 12);

            var crcBytes = new byte[4];
            if (await stream.ReadAsync(crcBytes, 0, 4) != 4)
                throw new InvalidOperationException("Couldn't read the crc");
            int checksum = BitConverter.ToInt32(crcBytes, 0);

            byte[] rv = new byte[packetLengthBytes.Length + seqBytes.Length + body.Length];

            Buffer.BlockCopy(packetLengthBytes, 0, rv, 0, packetLengthBytes.Length);
            Buffer.BlockCopy(seqBytes, 0, rv, packetLengthBytes.Length, seqBytes.Length);
            Buffer.BlockCopy(body, 0, rv, packetLengthBytes.Length + seqBytes.Length, body.Length);
            var crc32 = new Ionic.Crc.CRC32();
            crc32.SlurpBlock(rv, 0, rv.Length);
            var validChecksum = crc32.Crc32Result;

            if (checksum != validChecksum)
            {
                throw new InvalidOperationException("invalid checksum! skip");
            }

            return new TcpMessage(seq, body);
        }

        public static byte[] PrepareToSend(byte[] data)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var binaryWriter = new BinaryWriter(memoryStream))
                {
                    binaryWriter.Write((long)0);
                    binaryWriter.Write(GetNewMessageId());
                    binaryWriter.Write(data.Length);
                    binaryWriter.Write(data);

                    byte[] packet = memoryStream.ToArray();

                    return packet;
                }
            }
        }

        private static long GetNewMessageId()
        {
            long time = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds);
            long newMessageId = ((time / 1000 + timeOffset) << 32) |
                                ((time % 1000) << 22) |
                                (random.Next(524288) << 2); // 2^19
                                                            // [ unix timestamp : 32 bit] [ milliseconds : 10 bit ] [ buffer space : 1 bit ] [ random : 19 bit ] [ msg_id type : 2 bit ] = [ msg_id : 64 bit ]

            if (lastMessageId >= newMessageId)
            {
                newMessageId = lastMessageId + 4;
            }

            lastMessageId = newMessageId;
            return newMessageId;
        }

        private Tuple<byte[], ulong, int> DecodeMessage(byte[] body)
        {
            byte[] message;
            ulong remoteMessageId;
            int remoteSequence;

            using (var inputStream = new MemoryStream(body))
            using (var inputReader = new BinaryReader(inputStream))
            {
                if (inputReader.BaseStream.Length < 8)
                    throw new InvalidOperationException($"Can't decode packet");

                ulong remoteAuthKeyId = inputReader.ReadUInt64(); // TODO: check auth key id
                byte[] msgKey = inputReader.ReadBytes(16); // TODO: check msg_key correctness
                AESKeyData keyData = Helpers.CalcKey(body, msgKey, false);

                byte[] plaintext = AES.DecryptAES(keyData, inputReader.ReadBytes((int)(inputStream.Length - inputStream.Position)));

                using (MemoryStream plaintextStream = new MemoryStream(plaintext))
                using (BinaryReader plaintextReader = new BinaryReader(plaintextStream))
                {
                    var remoteSalt = plaintextReader.ReadUInt64();
                    var remoteSessionId = plaintextReader.ReadUInt64();
                    remoteMessageId = plaintextReader.ReadUInt64();
                    remoteSequence = plaintextReader.ReadInt32();
                    int msgLen = plaintextReader.ReadInt32();
                    message = plaintextReader.ReadBytes(msgLen);
                }
            }
            return new Tuple<byte[], ulong, int>(message, remoteMessageId, remoteSequence);
        }

        private static byte[] Encode(byte[] body, int sequneceNumber)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var binaryWriter = new BinaryWriter(memoryStream))
                {
                    // https://core.telegram.org/mtproto#tcp-transport
                    /*
                        4 length bytes are added at the front 
                        (to include the length, the sequence number, and CRC32; always divisible by 4)
                        and 4 bytes with the packet sequence number within this TCP connection 
                        (the first packet sent is numbered 0, the next one 1, etc.),
                        and 4 CRC32 bytes at the end (length, sequence number, and payload together).
                    */
                    binaryWriter.Write(body.Length + 12);
                    binaryWriter.Write(sequneceNumber);
                    binaryWriter.Write(body);
                    var crc32 = new CRC32();
                    crc32.SlurpBlock(memoryStream.GetBuffer(), 0, 8 + body.Length);
                    binaryWriter.Write(crc32.Crc32Result);

                    var transportPacket = memoryStream.ToArray();

                    //					Debug.WriteLine("Tcp packet #{0}\n{1}", SequneceNumber, BitConverter.ToString(transportPacket));

                    return transportPacket;
                }
            }
        }

        public static T FromByteArray<T>(byte[] data)
        {
            if (data == null)
                return default(T);
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream(data))
            {
                var obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }
    }
}
