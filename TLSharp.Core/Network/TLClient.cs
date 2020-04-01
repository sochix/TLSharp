using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TLSharp.Core.Network
{
    class TLClient : TcpClient
    {
        public delegate void ReceiveQueue(TcpMessage message);
        private readonly ReceiveQueue receiveQueue;
        public TLClient(IPAddress address, int port, ReceiveQueue queue) : base(address, port)
        {
            receiveQueue = queue;
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            var packetLengthBytes = new byte[4];
            packetLengthBytes = buffer.Take(4).ToArray();
            int packetLength = BitConverter.ToInt32(packetLengthBytes, 0);

            var seqBytes = new byte[4];
            seqBytes = buffer.Skip(4).Take(4).ToArray();
            int seq = BitConverter.ToInt32(seqBytes, 0);

            int readBytes = 0;
            var body = new byte[packetLength - 12];
            body = buffer.Skip(8).Take(packetLength - 12).ToArray();

            var crcBytes = new byte[4];
            crcBytes = buffer.Skip(packetLength - 4).Take(4).ToArray();
            int checksum = BitConverter.ToInt32(crcBytes, 0);

            byte[] rv = new byte[packetLengthBytes.Length + seqBytes.Length + body.Length];

            System.Buffer.BlockCopy(packetLengthBytes, 0, rv, 0, packetLengthBytes.Length);
            System.Buffer.BlockCopy(seqBytes, 0, rv, packetLengthBytes.Length, seqBytes.Length);
            System.Buffer.BlockCopy(body, 0, rv, packetLengthBytes.Length + seqBytes.Length, body.Length);
            var crc32 = new Ionic.Crc.CRC32();
            crc32.SlurpBlock(rv, 0, rv.Length);
            var validChecksum = crc32.Crc32Result;

            if (checksum != validChecksum)
            {
                throw new InvalidOperationException("invalid checksum! skip");
            }

            receiveQueue(new TcpMessage(seq, body));
        }


    }
}
