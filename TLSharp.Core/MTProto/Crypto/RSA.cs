using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace TLSharp.Core.MTProto.Crypto
{

    class RSAServerKey
    {

        private string fingerprint;
        private BigInteger m;
        private BigInteger e;

        public RSAServerKey(string fingerprint, BigInteger m, BigInteger e)
        {
            this.fingerprint = fingerprint;
            this.m = m;
            this.e = e;
        }

        public byte[] Encrypt(byte[] data, int offset, int length)
        {

            using (MemoryStream buffer = new MemoryStream(255))
            using (BinaryWriter writer = new BinaryWriter(buffer))
            {
                using (SHA1 sha1 = new SHA1Managed())
                {
                    byte[] hashsum = sha1.ComputeHash(data, offset, length);
                    writer.Write(hashsum);
                }

                buffer.Write(data, offset, length);
                if (length < 235)
                {
                    byte[] padding = new byte[235 - length];
                    new Random().NextBytes(padding);
                    buffer.Write(padding, 0, padding.Length);
                }

                byte[] ciphertext = new BigInteger(1, buffer.ToArray()).ModPow(e, m).ToByteArrayUnsigned();

                if (ciphertext.Length == 256)
                {
                    return ciphertext;
                }
                else {
                    byte[] paddedCiphertext = new byte[256];
                    int padding = 256 - ciphertext.Length;
                    for (int i = 0; i < padding; i++)
                    {
                        paddedCiphertext[i] = 0;
                    }
                    ciphertext.CopyTo(paddedCiphertext, padding);
                    return paddedCiphertext;
                }
            }

        }
    }
    public class RSA
    {
        private static readonly Dictionary<string, RSAServerKey> serverKeys = new Dictionary<string, RSAServerKey>() {
            { "216be86c022bb4c3", new RSAServerKey("216be86c022bb4c3", new BigInteger("00C150023E2F70DB7985DED064759CFECF0AF328E69A41DAF4D6F01B538135A6F91F8F8B2A0EC9BA9720CE352EFCF6C5680FFC424BD634864902DE0B4BD6D49F4E580230E3AE97D95C8B19442B3C0A10D8F5633FECEDD6926A7F6DAB0DDB7D457F9EA81B8465FCD6FFFEED114011DF91C059CAEDAF97625F6C96ECC74725556934EF781D866B34F011FCE4D835A090196E9A5F0E4449AF7EB697DDB9076494CA5F81104A305B6DD27665722C46B60E5DF680FB16B210607EF217652E60236C255F6A28315F4083A96791D7214BF64C1DF4FD0DB1944FB26A2A57031B32EEE64AD15A8BA68885CDE74A5BFC920F6ABF59BA5C75506373E7130F9042DA922179251F", 16), new BigInteger("010001", 16)) }
        };

        public static byte[] Encrypt(string fingerprint, byte[] data, int offset, int length)
        {
            string fingerprintLower = fingerprint.ToLower();
            if (!serverKeys.ContainsKey(fingerprintLower))
            {
                return null;
            }

            RSAServerKey key = serverKeys[fingerprintLower];

            return key.Encrypt(data, offset, length);
        }
    }

}
