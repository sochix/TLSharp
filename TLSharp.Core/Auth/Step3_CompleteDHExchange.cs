using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using TLSharp.Core.MTProto;
using TLSharp.Core.MTProto.Crypto;

namespace TLSharp.Core.Auth
{
    public class Step3_Response
    {
        public AuthKey AuthKey { get; set; }
        public int TimeOffset { get; set; }

    }

    public class Step3_CompleteDHExchange
    {
        private BigInteger _gab;
        private byte[] newNonce;
        private int timeOffset;

        public byte[] ToBytes(byte[] nonce, byte[] serverNonce, byte[] newNonce, byte[] encryptedAnswer)
        {
            this.newNonce = newNonce;
            AESKeyData key = AES.GenerateKeyDataFromNonces(serverNonce, newNonce);
            byte[] plaintextAnswer = AES.DecryptAES(key, encryptedAnswer);

            // logger.debug("plaintext answer: {0}", BitConverter.ToString(plaintextAnswer));

            int g;
            BigInteger dhPrime;
            BigInteger ga;

            using (MemoryStream dhInnerData = new MemoryStream(plaintextAnswer))
            {
                using (BinaryReader dhInnerDataReader = new BinaryReader(dhInnerData))
                {
                    byte[] hashsum = dhInnerDataReader.ReadBytes(20);
                    uint code = dhInnerDataReader.ReadUInt32();
                    if (code != 0xb5890dba)
                    {
                        throw new InvalidOperationException($"invalid dh_inner_data code: {code}");
                    }

                    // logger.debug("valid code");

                    byte[] nonceFromServer1 = dhInnerDataReader.ReadBytes(16);
                    if (!nonceFromServer1.SequenceEqual(nonce))
                    {
                        throw new InvalidOperationException("invalid nonce in encrypted answer");
                    }

                    // logger.debug("valid nonce");

                    byte[] serverNonceFromServer1 = dhInnerDataReader.ReadBytes(16);
                    if (!serverNonceFromServer1.SequenceEqual(serverNonce))
                    {
                        throw new InvalidOperationException("invalid server nonce in encrypted answer");
                    }

                    // logger.debug("valid server nonce");

                    g = dhInnerDataReader.ReadInt32();
                    dhPrime = new BigInteger(1, Serializers.Bytes.Read(dhInnerDataReader));
                    ga = new BigInteger(1, Serializers.Bytes.Read(dhInnerDataReader));

                    int serverTime = dhInnerDataReader.ReadInt32();
                    timeOffset = serverTime - (int)(Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds) / 1000);

                    // logger.debug("g: {0}, dhprime: {1}, ga: {2}", g, dhPrime, ga);
                }
            }

            BigInteger b = new BigInteger(2048, new Random());
            BigInteger gb = BigInteger.ValueOf(g).ModPow(b, dhPrime);
            _gab = ga.ModPow(b, dhPrime);

            // logger.debug("gab: {0}", gab);

            // prepare client dh inner data
            byte[] clientDHInnerDataBytes;
            using (MemoryStream clientDhInnerData = new MemoryStream())
            {
                using (BinaryWriter clientDhInnerDataWriter = new BinaryWriter(clientDhInnerData))
                {
                    clientDhInnerDataWriter.Write(0x6643b654); // client_dh_inner_data
                    clientDhInnerDataWriter.Write(nonce);
                    clientDhInnerDataWriter.Write(serverNonce);
                    clientDhInnerDataWriter.Write((long)0); // TODO: retry_id
                    Serializers.Bytes.Write(clientDhInnerDataWriter, gb.ToByteArrayUnsigned());

                    using (MemoryStream clientDhInnerDataWithHash = new MemoryStream())
                    {
                        using (BinaryWriter clientDhInnerDataWithHashWriter = new BinaryWriter(clientDhInnerDataWithHash))
                        {
                            using (SHA1 sha1 = new SHA1Managed())
                            {
                                clientDhInnerDataWithHashWriter.Write(sha1.ComputeHash(clientDhInnerData.GetBuffer(), 0, (int)clientDhInnerData.Position));
                                clientDhInnerDataWithHashWriter.Write(clientDhInnerData.GetBuffer(), 0, (int)clientDhInnerData.Position);
                                clientDHInnerDataBytes = clientDhInnerDataWithHash.ToArray();
                            }
                        }
                    }
                }
            }

            // logger.debug("client dh inner data papared len {0}: {1}", clientDHInnerDataBytes.Length, BitConverter.ToString(clientDHInnerDataBytes).Replace("-", ""));

            // encryption
            byte[] clientDhInnerDataEncryptedBytes = AES.EncryptAES(key, clientDHInnerDataBytes);

            // logger.debug("inner data encrypted {0}: {1}", clientDhInnerDataEncryptedBytes.Length, BitConverter.ToString(clientDhInnerDataEncryptedBytes).Replace("-", ""));

            // prepare set_client_dh_params
            byte[] setclientDhParamsBytes;
            using (MemoryStream setClientDhParams = new MemoryStream())
            {
                using (BinaryWriter setClientDhParamsWriter = new BinaryWriter(setClientDhParams))
                {
                    setClientDhParamsWriter.Write(0xf5045f1f);
                    setClientDhParamsWriter.Write(nonce);
                    setClientDhParamsWriter.Write(serverNonce);
                    Serializers.Bytes.Write(setClientDhParamsWriter, clientDhInnerDataEncryptedBytes);

                    setclientDhParamsBytes = setClientDhParams.ToArray();
                }
            }

            // logger.debug("set client dh params prepared: {0}", BitConverter.ToString(setclientDhParamsBytes));

            return setclientDhParamsBytes;
        }

        public Step3_Response FromBytes(byte[] response)
        {
            using (MemoryStream responseStream = new MemoryStream(response))
            {
                using (BinaryReader responseReader = new BinaryReader(responseStream))
                {
                    uint code = responseReader.ReadUInt32();
                    if (code == 0x3bcbf734)
                    { // dh_gen_ok
                      //logger.debug("dh_gen_ok");


                        byte[] nonceFromServer = responseReader.ReadBytes(16);
                        // TODO
                        /*
						if (!nonceFromServer.SequenceEqual(nonce))
						{
							logger.error("invalid nonce");
							return null;
						}
						*/

                        byte[] serverNonceFromServer = responseReader.ReadBytes(16);

                        // TODO:

                        /*
						if (!serverNonceFromServer.SequenceEqual(serverNonce))
						{
							logger.error("invalid server nonce");
							return null;
						}
						*/

                        byte[] newNonceHash1 = responseReader.ReadBytes(16);
                        //logger.debug("new nonce hash 1: {0}", BitConverter.ToString(newNonceHash1));

                        AuthKey authKey = new AuthKey(_gab);

                        byte[] newNonceHashCalculated = authKey.CalcNewNonceHash(newNonce, 1);

                        if (!newNonceHash1.SequenceEqual(newNonceHashCalculated))
                        {
                            throw new InvalidOperationException("invalid new nonce hash");
                        }

                        //logger.info("generated new auth key: {0}", gab);
                        //logger.info("saving time offset: {0}", timeOffset);
                        //TelegramSession.Instance.TimeOffset = timeOffset;

                        return new Step3_Response()
                        {
                            AuthKey = authKey,
                            TimeOffset = timeOffset
                        };
                    }
                    else if (code == 0x46dc1fb9)
                    { // dh_gen_retry
                        throw new NotImplementedException("dh_gen_retry");

                    }
                    else if (code == 0xa69dae02)
                    {
                        // dh_gen_fail
                        throw new NotImplementedException("dh_gen_fail");
                    }
                    else
                    {
                        throw new InvalidOperationException($"dh_gen unknown: {code}");
                    }
                }
            }
        }
    }
}
