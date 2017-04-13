using System;
using System.IO;
using TeleSharp.TL;
using TLSharp.Core.Utils;

namespace TLSharp.Core.Requests
{
    public class PingRequest : TLMethod
    {
        public override int Constructor => 0x7abe77ec;

        public override void SerializeBody(BinaryWriter writer)
        {
            writer.Write(Constructor);
            writer.Write(Helpers.GenerateRandomLong());
        }

        public override void DeserializeBody(BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public override void deserializeResponse(BinaryReader stream)
        {
            throw new NotImplementedException();
        }
    }
}