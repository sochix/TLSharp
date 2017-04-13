using System;
using System.Collections.Generic;
using System.IO;
using TLSharp.Core.MTProto;
using TLSharp.Core.MTProto.Crypto;

namespace TLSharp.Core.Auth
{
    public class Step2_Response
    {
        public byte[] Nonce { get; set; }
        public byte[] ServerNonce { get; set; }
        public byte[] NewNonce { get; set; }
        public byte[] EncryptedAnswer { get; set; }
    }

    public class Step2_DHExchange
    {
        public byte[] newNonce;

        public Step2_DHExchange()
        {
            newNonce = new byte[32];
        }

        public byte[] ToBytes(byte[] nonce, byte[] serverNonce, List<byte[]> fingerprints, BigInteger pq)
        {
            new Random().NextBytes(newNonce);

            var pqPair = Factorizator.Factorize(pq);

            byte[] reqDhParamsBytes;

            using (var pqInnerData = new MemoryStream(255))
            {
                using (var pqInnerDataWriter = new BinaryWriter(pqInnerData))
                {
                    pqInnerDataWriter.Write(0x83c95aec); // pq_inner_data
                    Serializers.Bytes.write(pqInnerDataWriter, pq.ToByteArrayUnsigned());
                    Serializers.Bytes.write(pqInnerDataWriter, pqPair.Min.ToByteArrayUnsigned());
                    Serializers.Bytes.write(pqInnerDataWriter, pqPair.Max.ToByteArrayUnsigned());
                    pqInnerDataWriter.Write(nonce);
                    pqInnerDataWriter.Write(serverNonce);
                    pqInnerDataWriter.Write(newNonce);

                    byte[] ciphertext = null;
                    byte[] targetFingerprint = null;
                    foreach (var fingerprint in fingerprints)
                    {
                        ciphertext = RSA.Encrypt(BitConverter.ToString(fingerprint).Replace("-", string.Empty),
                            pqInnerData.GetBuffer(), 0, (int) pqInnerData.Position);
                        if (ciphertext != null)
                        {
                            targetFingerprint = fingerprint;
                            break;
                        }
                    }

                    if (ciphertext == null)
                        throw new InvalidOperationException(
                            string.Format("not found valid key for fingerprints: {0}",
                                string.Join(", ", fingerprints)));

                    using (var reqDHParams = new MemoryStream(1024))
                    {
                        using (var reqDHParamsWriter = new BinaryWriter(reqDHParams))
                        {
                            reqDHParamsWriter.Write(0xd712e4be); // req_dh_params
                            reqDHParamsWriter.Write(nonce);
                            reqDHParamsWriter.Write(serverNonce);
                            Serializers.Bytes.write(reqDHParamsWriter, pqPair.Min.ToByteArrayUnsigned());
                            Serializers.Bytes.write(reqDHParamsWriter, pqPair.Max.ToByteArrayUnsigned());
                            reqDHParamsWriter.Write(targetFingerprint);
                            Serializers.Bytes.write(reqDHParamsWriter, ciphertext);

                            reqDhParamsBytes = reqDHParams.ToArray();
                        }
                    }
                }
                return reqDhParamsBytes;
            }
        }

        public Step2_Response FromBytes(byte[] response)
        {
            byte[] encryptedAnswer;

            using (var responseStream = new MemoryStream(response, false))
            {
                using (var responseReader = new BinaryReader(responseStream))
                {
                    var responseCode = responseReader.ReadUInt32();

                    if (responseCode == 0x79cb045d)
                        throw new InvalidOperationException("server_DH_params_fail: TODO");

                    if (responseCode != 0xd0e8075c)
                        throw new InvalidOperationException($"invalid response code: {responseCode}");

                    var nonceFromServer = responseReader.ReadBytes(16);

                    // TODO:!
                    /*
					if (!nonceFromServer.SequenceEqual(nonce))
					{
						logger.debug("invalid nonce from server");
						return null;
					}
					*/


                    var serverNonceFromServer = responseReader.ReadBytes(16);

                    // TODO: !
                    /*
					if (!serverNonceFromServer.SequenceEqual(serverNonce))
					{
						logger.error("invalid server nonce from server");
						return null;
					}
					*/

                    encryptedAnswer = Serializers.Bytes.read(responseReader);

                    return new Step2_Response
                    {
                        EncryptedAnswer = encryptedAnswer,
                        ServerNonce = serverNonceFromServer,
                        Nonce = nonceFromServer,
                        NewNonce = newNonce
                    };
                }
            }
        }
    }
}