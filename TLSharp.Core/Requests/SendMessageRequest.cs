using System;
using System.IO;
using TLSharp.Core.MTProto;
using TLSharp.Core.Utils;

namespace TLSharp.Core.Requests
{
    public class SendMessageRequest : MTProtoRequest
    {
        private InputPeer _peer;
        private string _message;

        public SendMessageRequest(InputPeer peer, string message)
        {
            _peer = peer;
            _message = message;
        }

        public override void OnSend(BinaryWriter writer)
        {
            long random_id = Helpers.GenerateRandomLong();
            writer.Write(0x4cde0aab);
            _peer.Write(writer);
            Serializers.String.write(writer, _message);
            writer.Write(random_id);
        }

        public override void OnResponse(BinaryReader reader)
        {
            var code = reader.ReadUInt32();

            var id = reader.ReadInt32();
            var date = reader.ReadInt32();
            var pts = reader.ReadInt32();
            var seq = reader.ReadInt32();
        }

        public override void OnException(Exception exception)
        {
            throw new NotImplementedException();
        }

        public override bool Confirmed => true;
        public override bool Responded { get; }
    }
}
