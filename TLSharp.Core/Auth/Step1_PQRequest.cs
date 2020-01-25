using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TLSharp.Core.MTProto;
using TLSharp.Core.MTProto.Crypto;

namespace TLSharp.Core.Auth
{
    public class Step1_Response
    {
        public byte[] Nonce { get; set; }
        public byte[] ServerNonce { get; set; }
        public BigInteger Pq { get; set; }
        public List<byte[]> Fingerprints { get; set; }
    }

    public class Step1_PQRequest
    {
        private byte[] nonce;

        public Step1_PQRequest()
        {
            nonce = new byte[16];
        }

        public byte[] ToBytes()
        {
            new Random().NextBytes(nonce);
            const int constructorNumber = 0x60469778;

            using (var memoryStream = new MemoryStream())
            {
                using (var binaryWriter = new BinaryWriter(memoryStream))
                {
                    binaryWriter.Write(constructorNumber);
                    binaryWriter.Write(nonce);

                    return memoryStream.ToArray();
                }
            }
        }

        public Step1_Response FromBytes(byte[] bytes)
        {
            var fingerprints = new List<byte[]>();

            using (var memoryStream = new MemoryStream(bytes, false))
            {
                using (var binaryReader = new BinaryReader(memoryStream))
                {
                    const int responseConstructorNumber = 0x05162463;
                    var responseCode = binaryReader.ReadInt32();
                    if (responseCode != responseConstructorNumber)
                    {
                        throw new InvalidOperationException($"invalid response code: {responseCode}");
                    }

                    var nonceFromServer = binaryReader.ReadBytes(16);

                    if (!nonceFromServer.SequenceEqual(nonce))
                    {
                        throw new InvalidOperationException("invalid nonce from server");
                    }

                    var serverNonce = binaryReader.ReadBytes(16);

                    byte[] pqbytes = Serializers.Bytes.Read(binaryReader);
                    var pq = new BigInteger(1, pqbytes);

                    var vectorId = binaryReader.ReadInt32();
                    const int vectorConstructorNumber = 0x1cb5c415;
                    if (vectorId != vectorConstructorNumber)
                    {
                        throw new InvalidOperationException($"Invalid vector constructor number {vectorId}");
                    }

                    var fingerprintCount = binaryReader.ReadInt32();
                    for (var i = 0; i < fingerprintCount; i++)
                    {
                        byte[] fingerprint = binaryReader.ReadBytes(8);
                        fingerprints.Add(fingerprint);
                    }

                    return new Step1_Response
                    {
                        Fingerprints = fingerprints,
                        Nonce = nonce,
                        Pq = pq,
                        ServerNonce = serverNonce
                    };
                }
            }
        }
    }
}
