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
                else
                {
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

        public byte[] Decrypt(byte[] cipherdata, BigInteger d, int offset, int length)
        {
            byte[] text = new BigInteger(1, cipherdata).ModPow(d, m).ToByteArrayUnsigned();
            return text;
        }
    }
    public class RSA
    {
        private static readonly Dictionary<string, RSAServerKey> serverKeys = new Dictionary<string, RSAServerKey>() {
            { "216be86c022bb4c3_", new RSAServerKey("216be86c022bb4c3_", new BigInteger("00C150023E2F70DB7985DED064759CFECF0AF328E69A41DAF4D6F01B538135A6F91F8F8B2A0EC9BA9720CE352EFCF6C5680FFC424BD634864902DE0B4BD6D49F4E580230E3AE97D95C8B19442B3C0A10D8F5633FECEDD6926A7F6DAB0DDB7D457F9EA81B8465FCD6FFFEED114011DF91C059CAEDAF97625F6C96ECC74725556934EF781D866B34F011FCE4D835A090196E9A5F0E4449AF7EB697DDB9076494CA5F81104A305B6DD27665722C46B60E5DF680FB16B210607EF217652E60236C255F6A28315F4083A96791D7214BF64C1DF4FD0DB1944FB26A2A57031B32EEE64AD15A8BA68885CDE74A5BFC920F6ABF59BA5C75506373E7130F9042DA922179251F", 16), new BigInteger("010001", 16)) },
            { "216be86c022bb4c3", new RSAServerKey("216be86c022bb4c3", new BigInteger("E5F6E2F9781DD46D169B6F072382A1443D10F22D31CE5F007B3B2FAE37BD9EAB3B69A76E9CA75D8C596CCA70A9252D67562199E460F3644A3BFBAE61405B7AF92ED0F8BD1E09D4765BFE64C8CF1391E6A69D3EB3E8B42538A33EAC86613EE76B3EF5F5CBB58DAA17E53E346F5F5EB20E2F8C5F5D0EEF9FED78E0399689C1B209B34730CBFE022E2BC2FD6A2B5A76FD8D5B3BCB1AC10F9D88CA6DEBB4E45F91460EC3C46D3B03AAB8438C8F294AAE852A7C9A77997BEE02773788CB66619F8994A4E4541FC6E652BE7349A2D7DEBD2A8D6FC6A90371D544682EA1CEB9FBFE4B31AC17753945A3B598A22B927FFF7ED00600772F5041A86A2DD20D4623EA9A0D6D", 16), new BigInteger("010001", 16)) }
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

        public static byte[] Decrypt(string fingerprint, byte[] cipherdata, BigInteger d, int offset, int length)
        {
            string fingerprintLower = fingerprint.ToLower();
            if (!serverKeys.ContainsKey(fingerprintLower))
            {
                return null;
            }

            RSAServerKey key = serverKeys[fingerprintLower];

            return key.Decrypt(cipherdata, d, offset, length);
        }
    }

}
