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
            { "216be86c022bb4c3", new RSAServerKey("216be86c022bb4c3", new BigInteger("00C150023E2F70DB7985DED064759CFECF0AF328E69A41DAF4D6F01B538135A6F91F8F8B2A0EC9BA9720CE352EFCF6C5680FFC424BD634864902DE0B4BD6D49F4E580230E3AE97D95C8B19442B3C0A10D8F5633FECEDD6926A7F6DAB0DDB7D457F9EA81B8465FCD6FFFEED114011DF91C059CAEDAF97625F6C96ECC74725556934EF781D866B34F011FCE4D835A090196E9A5F0E4449AF7EB697DDB9076494CA5F81104A305B6DD27665722C46B60E5DF680FB16B210607EF217652E60236C255F6A28315F4083A96791D7214BF64C1DF4FD0DB1944FB26A2A57031B32EEE64AD15A8BA68885CDE74A5BFC920F6ABF59BA5C75506373E7130F9042DA922179251F", 16), new BigInteger("010001", 16)) },
			{ "9b721cb11d6a999a", new RSAServerKey("9b721cb11d6a999a", new BigInteger("00C6AEDA78B02A251DB4B6441031F467FA871FAED32526C436524B1FB3B5DCA28EFB8C089DD1B46D92C895993D87108254951C5F001A0F055F3063DCD14D431A300EB9E29517E359A1C9537E5E87AB1B116FAECF5D17546EBC21DB234D9D336A693EFCB2B6FBCCA1E7D1A0BE414DCA408A11609B9C4269A920B09FED1F9A1597BE02761430F09E4BC48FCAFBE289054C99DBA51B6B5EB7D9C3A2AB4E490545B4676BD620E93804BCAC93BF94F73F92C729CA899477FF17625EF14A934D51DC11D5F8650A3364586B3A52FCFF2FEDEC8A8406CAC4E751705A472E55707E3C8CD5594342B119C6C3293532D85DBE9271ED54A2FD18B4DC79C04A30951107D5639397", 16), new BigInteger("010001", 16)) },
			{ "78eacd706f2a5bb0", new RSAServerKey("78eacd706f2a5bb0", new BigInteger("00B1066749655935F0A5936F517034C943BEA7F3365A8931AE52C8BCB14856F004B83D26CF2839BE0F22607470D67481771C1CE5EC31DE16B20BBAA4ECD2F7D2ECF6B6356F27501C226984263EDC046B89FB6D3981546B01D7BD34FEDCFCC1058E2D494BDA732FF813E50E1C6AE249890B225F82B22B1E55FCB063DC3C0E18E91C28D0C4AA627DEC8353EEE6038A95A4FD1CA984EB09F94AEB7A2220635A8CEB450EA7E61D915CDB4EECEDAA083AA3801DAF071855EC1FB38516CB6C2996D2D60C0ECBCFA57E4CF1FB0ED39B2F37E94AB4202ECF595E167B3CA62669A6DA520859FB6D6C6203DFDFC79C75EC3EE97DA8774B2DA903E3435F2CD294670A75A526C1", 16), new BigInteger("010001", 16)) },
			{ "e33360c7b625e071", new RSAServerKey("e33360c7b625e071", new BigInteger("00C2A8C55B4A62E2B78A19B91CF692BCDC4BA7C23FE4D06F194E2A0C30F6D9996F7D1A2BCC89BC1AC4333D44359A6C433252D1A8402D9970378B5912B75BC8CC3FA76710A025BCB9032DF0B87D7607CC53B928712A174EA2A80A8176623588119D42FFCE40205C6D72160860D8D80B22A8B8651907CF388EFFBEF29CD7CF2B4EB8A872052DA1351CFE7FEC214CE48304EA472BD66329D60115B3420D08F6894B0410B6AB9450249967617670C932F7CBDB5D6FBCCE1E492C595F483109999B2661FCDEEC31B196429B7834C7211A93C6789D9EE601C18C39E521FDA9D7264E61E518ADD6F0712D2D5228204B851E13C4F322E5C5431C3B7F31089668486AADC59F", 16), new BigInteger("010001", 16)) }
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
