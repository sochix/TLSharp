using System;
using System.IO;
using TLSharp.Core.MTProto;

namespace TLSharp.Core.Requests
{
    public class GetUpdatesStateRequest : MTProtoRequest
    {
        public updates_State updates { get; private set; }

        public override void OnSend(BinaryWriter writer)
        {
            writer.Write(0xedd4882a);
        }

        public override void OnResponse(BinaryReader reader)
        {
            updates = TL.Parse<updates_State>(reader);
        }

        public override void OnException(Exception exception)
        {
            throw new NotImplementedException();
        }

        public override bool Confirmed => true;
        public override bool Responded { get; }
    }
}