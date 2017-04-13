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
            var key = AES.GenerateKeyDataFromNonces(serverNonce, newNonce);
            var plaintextAnswer = AES.DecryptAES(key, encryptedAnswer);

            // logger.debug("plaintext answer: {0}", BitConverter.ToString(plaintextAnswer));

            int g;
            BigInteger dhPrime;
            BigInteger ga;

            using (var dhInnerData = new MemoryStream(plaintextAnswer))
            {
                using (var dhInnerDataReader = new BinaryReader(dhInnerData))
                {
                    var hashsum = dhInnerDataReader.ReadBytes(20);
                    var code = dhInnerDataReader.ReadUInt32();
                    if (code != 0xb5890dba)
                        throw new InvalidOperationException($"invalid dh_inner_data code: {code}");

                    // logger.debug("valid code");

                    var nonceFromServer1 = dhInnerDataReader.ReadBytes(16);
                    if (!nonceFromServer1.SequenceEqual(nonce))
                        throw new InvalidOperationException("invalid nonce in encrypted answer");

                    // logger.debug("valid nonce");

                    var serverNonceFromServer1 = dhInnerDataReader.ReadBytes(16);
                    if (!serverNonceFromServer1.SequenceEqual(serverNonce))
                        throw new InvalidOperationException("invalid server nonce in encrypted answer");

                    // logger.debug("valid server nonce");

                    g = dhInnerDataReader.ReadInt32();
                    dhPrime = new BigInteger(1, Serializers.Bytes.read(dhInnerDataReader));
                    ga = new BigInteger(1, Serializers.Bytes.read(dhInnerDataReader));

                    var serverTime = dhInnerDataReader.ReadInt32();
                    timeOffset = serverTime -
                                 (int) (Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1))
                                            .TotalMilliseconds) / 1000);

                    // logger.debug("g: {0}, dhprime: {1}, ga: {2}", g, dhPrime, ga);
                }
            }

            var b = new BigInteger(2048, new Random());
            var gb = BigInteger.ValueOf(g).ModPow(b, dhPrime);
            _gab = ga.ModPow(b, dhPrime);

            // logger.debug("gab: {0}", gab);

            // prepare client dh inner data
            byte[] clientDHInnerDataBytes;
            using (var clientDhInnerData = new MemoryStream())
            {
                using (var clientDhInnerDataWriter = new BinaryWriter(clientDhInnerData))
                {
                    clientDhInnerDataWriter.Write(0x6643b654); // client_dh_inner_data
                    clientDhInnerDataWriter.Write(nonce);
                    clientDhInnerDataWriter.Write(serverNonce);
                    clientDhInnerDataWriter.Write((long) 0); // TODO: retry_id
                    Serializers.Bytes.write(clientDhInnerDataWriter, gb.ToByteArrayUnsigned());

                    using (var clientDhInnerDataWithHash = new MemoryStream())
                    {
                        using (var clientDhInnerDataWithHashWriter = new BinaryWriter(clientDhInnerDataWithHash))
                        {
                            using (SHA1 sha1 = new SHA1Managed())
                            {
                                clientDhInnerDataWithHashWriter.Write(sha1.ComputeHash(clientDhInnerData.GetBuffer(), 0,
                                    (int) clientDhInnerData.Position));
                                clientDhInnerDataWithHashWriter.Write(clientDhInnerData.GetBuffer(), 0,
                                    (int) clientDhInnerData.Position);
                                clientDHInnerDataBytes = clientDhInnerDataWithHash.ToArray();
                            }
                        }
                    }
                }
            }

            // logger.debug("client dh inner data papared len {0}: {1}", clientDHInnerDataBytes.Length, BitConverter.ToString(clientDHInnerDataBytes).Replace("-", ""));

            // encryption
            var clientDhInnerDataEncryptedBytes = AES.EncryptAES(key, clientDHInnerDataBytes);

            // logger.debug("inner data encrypted {0}: {1}", clientDhInnerDataEncryptedBytes.Length, BitConverter.ToString(clientDhInnerDataEncryptedBytes).Replace("-", ""));

            // prepare set_client_dh_params
            byte[] setclientDhParamsBytes;
            using (var setClientDhParams = new MemoryStream())
            {
                using (var setClientDhParamsWriter = new BinaryWriter(setClientDhParams))
                {
                    setClientDhParamsWriter.Write(0xf5045f1f);
                    setClientDhParamsWriter.Write(nonce);
                    setClientDhParamsWriter.Write(serverNonce);
                    Serializers.Bytes.write(setClientDhParamsWriter, clientDhInnerDataEncryptedBytes);

                    setclientDhParamsBytes = setClientDhParams.ToArray();
                }
            }

            // logger.debug("set client dh params prepared: {0}", BitConverter.ToString(setclientDhParamsBytes));

            return setclientDhParamsBytes;
        }

        public Step3_Response FromBytes(byte[] response)
        {
            using (var responseStream = new MemoryStream(response))
            {
                using (var responseReader = new BinaryReader(responseStream))
                {
                    var code = responseReader.ReadUInt32();
                    if (code == 0x3bcbf734)
                    {
                        // dh_gen_ok
                        //logger.debug("dh_gen_ok");


                        var nonceFromServer = responseReader.ReadBytes(16);
                        // TODO
                        /*
						if (!nonceFromServer.SequenceEqual(nonce))
						{
							logger.error("invalid nonce");
							return null;
						}
						*/

                        var serverNonceFromServer = responseReader.ReadBytes(16);

                        // TODO:

                        /*
						if (!serverNonceFromServer.SequenceEqual(serverNonce))
						{
							logger.error("invalid server nonce");
							return null;
						}
						*/

                        var newNonceHash1 = responseReader.ReadBytes(16);
                        //logger.debug("new nonce hash 1: {0}", BitConverter.ToString(newNonceHash1));

                        var authKey = new AuthKey(_gab);

                        var newNonceHashCalculated = authKey.CalcNewNonceHash(newNonce, 1);

                        if (!newNonceHash1.SequenceEqual(newNonceHashCalculated))
                            throw new InvalidOperationException("invalid new nonce hash");

                        //logger.info("generated new auth key: {0}", gab);
                        //logger.info("saving time offset: {0}", timeOffset);
                        //TelegramSession.Instance.TimeOffset = timeOffset;

                        return new Step3_Response
                        {
                            AuthKey = authKey,
                            TimeOffset = timeOffset
                        };
                    }
                    if (code == 0x46dc1fb9)
                        throw new NotImplementedException("dh_gen_retry");
                    if (code == 0xa69dae02)
                        throw new NotImplementedException("dh_gen_fail");
                    throw new InvalidOperationException($"dh_gen unknown: {code}");
                }
            }
        }
    }
}