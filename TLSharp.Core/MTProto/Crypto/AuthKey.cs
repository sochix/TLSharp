using System;
using System.IO;
using System.Security.Cryptography;

namespace TLSharp.Core.MTProto.Crypto
{
    public class AuthKey
    {
        private byte[] key;
        private ulong keyId;
        private ulong auxHash;
        public AuthKey(BigInteger gab)
        {
            key = gab.ToByteArrayUnsigned();
            using (SHA1 hash = new SHA1Managed())
            {
                using (MemoryStream hashStream = new MemoryStream(hash.ComputeHash(key), false))
                {
                    using (BinaryReader hashReader = new BinaryReader(hashStream))
                    {
                        auxHash = hashReader.ReadUInt64();
                        hashReader.ReadBytes(4);
                        keyId = hashReader.ReadUInt64();
                    }
                }
            }
        }

        public AuthKey(byte[] data)
        {
            key = data;
            using (SHA1 hash = new SHA1Managed())
            {
                using (MemoryStream hashStream = new MemoryStream(hash.ComputeHash(key), false))
                {
                    using (BinaryReader hashReader = new BinaryReader(hashStream))
                    {
                        auxHash = hashReader.ReadUInt64();
                        hashReader.ReadBytes(4);
                        keyId = hashReader.ReadUInt64();
                    }
                }
            }
        }

        public byte[] CalcNewNonceHash(byte[] newNonce, int number)
        {
            using (MemoryStream buffer = new MemoryStream(100))
            {
                using (BinaryWriter bufferWriter = new BinaryWriter(buffer))
                {
                    bufferWriter.Write(newNonce);
                    bufferWriter.Write((byte)number);
                    bufferWriter.Write(auxHash);
                    using (SHA1 sha1 = new SHA1Managed())
                    {
                        byte[] hash = sha1.ComputeHash(buffer.GetBuffer(), 0, (int)buffer.Position);
                        byte[] newNonceHash = new byte[16];
                        Array.Copy(hash, 4, newNonceHash, 0, 16);
                        return newNonceHash;
                    }
                }
            }
        }

        public byte[] Data
        {
            get
            {
                return key;
            }
        }

        public ulong Id
        {
            get
            {
                return keyId;
            }
        }

        public override string ToString()
        {
            return string.Format("(Key: {0}, KeyId: {1}, AuxHash: {2})", key, keyId, auxHash);
        }
    }
}
