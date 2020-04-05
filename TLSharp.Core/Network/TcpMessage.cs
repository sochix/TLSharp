using System;
using System.IO;
using System.Linq;
using TLSharp.Core.MTProto.Crypto;

namespace TLSharp.Core.Network
{
    public class TcpMessage
    {
        public int SequneceNumber { get; private set; }
        public byte[] Body { get; private set; }

        public TcpMessage(int seqNumber, byte[] body)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            SequneceNumber = seqNumber;
            Body = body;
        }

        public byte[] Encode()
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
                    binaryWriter.Write(Body.Length + 12);
                    binaryWriter.Write(SequneceNumber);
                    binaryWriter.Write(Body);
                    var crc32 = new Crc32();
                    var checksum = crc32.ComputeHash(memoryStream.GetBuffer(), 0, 8 + Body.Length).Reverse().ToArray();
                    binaryWriter.Write(checksum);

                    var transportPacket = memoryStream.ToArray();

                    //					Debug.WriteLine("Tcp packet #{0}\n{1}", SequneceNumber, BitConverter.ToString(transportPacket));

                    return transportPacket;
                }
            }
        }

        public static TcpMessage Decode(byte[] body)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if (body.Length < 12)
                throw new InvalidOperationException("Ops, wrong size of input packet");

            using (var memoryStream = new MemoryStream(body))
            {
                using (var binaryReader = new BinaryReader(memoryStream))
                {
                    var packetLength = binaryReader.ReadInt32();

                    if (packetLength < 12)
                        throw new InvalidOperationException(string.Format("invalid packet length: {0}", packetLength));

                    var seq = binaryReader.ReadInt32();
                    byte[] packet = binaryReader.ReadBytes(packetLength - 12);
                    var checksum = binaryReader.ReadBytes(4);

                    var crc32 = new Crc32();
                    var computedChecksum = crc32.ComputeHash(body, 0, packetLength - 4).Reverse();

                    if (!checksum.SequenceEqual(computedChecksum))
                    {
                        throw new InvalidOperationException("invalid checksum! skip");
                    }

                    return new TcpMessage(seq, packet);
                }
            }
        }
    }
}
