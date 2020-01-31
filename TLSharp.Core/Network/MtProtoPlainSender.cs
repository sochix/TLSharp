using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace TLSharp.Core.Network
{
    public class MtProtoPlainSender
    {
        private int timeOffset;
        private long lastMessageId;
        private Random random;
        private TcpTransport transport;

        public MtProtoPlainSender(TcpTransport transport)
        {
            this.transport = transport;
            random = new Random();
        }

        public async Task Send(byte[] data, CancellationToken token = default(CancellationToken))
        {
            token.ThrowIfCancellationRequested();

            using (var memoryStream = new MemoryStream())
            {
                using (var binaryWriter = new BinaryWriter(memoryStream))
                {
                    binaryWriter.Write((long)0);
                    binaryWriter.Write(GetNewMessageId());
                    binaryWriter.Write(data.Length);
                    binaryWriter.Write(data);

                    byte[] packet = memoryStream.ToArray();

                    await transport.Send(packet, token).ConfigureAwait(false);
                }
            }
        }

        public async Task<byte[]> Receive(CancellationToken token = default(CancellationToken))
        {
            token.ThrowIfCancellationRequested();

            var result = await transport.Receive(token).ConfigureAwait(false);

            using (var memoryStream = new MemoryStream(result.Body))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream))
                {
                    long authKeyid = binaryReader.ReadInt64();
                    long messageId = binaryReader.ReadInt64();
                    int messageLength = binaryReader.ReadInt32();

                    byte[] response = binaryReader.ReadBytes(messageLength);

                    return response;
                }
            }
        }

        private long GetNewMessageId()
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


    }
}
