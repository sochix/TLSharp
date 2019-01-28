using System.Threading.Tasks;
using TLSharp.Core.Network;

namespace TLSharp.Core.Auth
{
    public static class Authenticator
    {
        public static Step3_Response DoAuthentication(TcpTransport transport)
        {
            var sender = new MtProtoPlainSender(transport);
            var step1 = new Step1_PQRequest();

            sender.Send(step1.ToBytes());
            var step1Response = step1.FromBytes(sender.Receive());

            var step2 = new Step2_DHExchange();
            sender.Send(step2.ToBytes(
                step1Response.Nonce,
                step1Response.ServerNonce,
                step1Response.Fingerprints,
                step1Response.Pq));

            var step2Response = step2.FromBytes(sender.Receive());

            var step3 = new Step3_CompleteDHExchange();
            sender.Send(step3.ToBytes(
                step2Response.Nonce,
                step2Response.ServerNonce,
                step2Response.NewNonce,
                step2Response.EncryptedAnswer));

            var step3Response = step3.FromBytes(sender.Receive());

            return step3Response;
        }
    }
}
