using System.Threading;
using System.Threading.Tasks;
using TLSharp.Core.Network;

namespace TLSharp.Core.Auth
{
    public static class Authenticator
    {
        public static async Task<Step3_Response> DoAuthentication(TcpTransport transport, CancellationToken token = default(CancellationToken))
        {
            token.ThrowIfCancellationRequested();

            var sender = new MtProtoPlainSender(transport);
            var step1 = new Step1_PQRequest();

            await sender.Send(step1.ToBytes(), token).ConfigureAwait(false);
            var step1Response = step1.FromBytes(await sender.Receive(token)
                .ConfigureAwait(false));

            var step2 = new Step2_DHExchange();
            await sender.Send(step2.ToBytes(
                    step1Response.Nonce,
                    step1Response.ServerNonce,
                    step1Response.Fingerprints,
                    step1Response.Pq), token)
                .ConfigureAwait(false);

            var step2Response = step2.FromBytes(await sender.Receive(token)
                .ConfigureAwait(false));

            var step3 = new Step3_CompleteDHExchange();
            await sender.Send(step3.ToBytes(
                    step2Response.Nonce,
                    step2Response.ServerNonce,
                    step2Response.NewNonce,
                    step2Response.EncryptedAnswer), token)
                .ConfigureAwait(false);

            var step3Response = step3.FromBytes(await sender.Receive(token)
                .ConfigureAwait(false));

            return step3Response;
        }
    }
}