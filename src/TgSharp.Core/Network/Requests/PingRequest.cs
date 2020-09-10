using System;
using System.IO;

using TgSharp.TL;
using TgSharp.Core.Utils;

namespace TgSharp.Core.Network.Requests
{
    public class PingRequest : TLMethod
    {
        public PingRequest()
        {
        }

        public override void SerializeBody(BinaryWriter writer)
        {
            writer.Write(Constructor);
            writer.Write(Helpers.GenerateRandomLong());
        }

        public override void DeserializeBody(BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public override void DeserializeResponse(BinaryReader stream)
        {
            throw new NotImplementedException();
        }

        public override int Constructor
        {
            get
            {
                return 0x7abe77ec;
            }
        }
    }
}
