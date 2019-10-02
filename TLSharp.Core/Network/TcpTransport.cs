using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TLSharp.Core.Network
{
    public delegate TcpClient TcpClientConnectionHandler(string address, int port);

    public class TcpTransport : IDisposable
    {
        private readonly TcpClient _tcpClient; 
        private readonly NetworkStream _stream;
        private int sendCounter = 0;

        public TcpTransport(string address, int port, TcpClientConnectionHandler handler = null)
        {
            if (handler == null)
            {
                var ipAddress = IPAddress.Parse(address);
                var endpoint = new IPEndPoint(ipAddress, port);

                _tcpClient = new TcpClient(ipAddress.AddressFamily);
                _tcpClient.Connect(endpoint);
            }
            else
                _tcpClient = handler(address, port);

            if (_tcpClient.Connected)
            {
                _stream = _tcpClient.GetStream();
            }
        }

        public async Task Send(byte[] packet)
        {
            if (!_tcpClient.Connected)
                throw new InvalidOperationException("Client not connected to server.");

            var tcpMessage = new TcpMessage(sendCounter, packet);

            await _stream.WriteAsync(tcpMessage.Encode(), 0, tcpMessage.Encode().Length);
            sendCounter++;
        }

        public async Task<TcpMessage> Receive()
        {
            var packetLengthBytes = new byte[4];
            if (await _stream.ReadAsync(packetLengthBytes, 0, 4) != 4)
                throw new InvalidOperationException("Couldn't read the packet length");
            int packetLength = BitConverter.ToInt32(packetLengthBytes, 0);

            var seqBytes = new byte[4];
            if (await _stream.ReadAsync(seqBytes, 0, 4) != 4)
                throw new InvalidOperationException("Couldn't read the sequence");
            int seq = BitConverter.ToInt32(seqBytes, 0);

            int readBytes = 0;
            var body = new byte[packetLength - 12];
            int neededToRead = packetLength - 12;

            do
            {
                var bodyByte = new byte[packetLength - 12];
                var availableBytes = await _stream.ReadAsync(bodyByte, 0, neededToRead);
                neededToRead -= availableBytes;
                Buffer.BlockCopy(bodyByte, 0, body, readBytes, availableBytes);
                readBytes += availableBytes;
            }
            while (readBytes != packetLength - 12);

            var crcBytes = new byte[4];
            if (await _stream.ReadAsync(crcBytes, 0, 4) != 4)
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

        public bool IsConnected
        {
            get
            {
                return this._tcpClient.Connected;
            }
        }


        public void Dispose()
        {
            if (_tcpClient.Connected)
            {
                _stream.Close();
                _tcpClient.Close();
            }
        }
    }
}
