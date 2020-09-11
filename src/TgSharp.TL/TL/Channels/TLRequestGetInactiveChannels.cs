using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL.Channels
{
    [TLObject(300429806)]
    public class TLRequestGetInactiveChannels : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 300429806;
            }
        }

        public Messages.TLInactiveChats Response { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            // do nothing
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            // do nothing else
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLInactiveChats)ObjectUtils.DeserializeObject(br);
        }
    }
}
