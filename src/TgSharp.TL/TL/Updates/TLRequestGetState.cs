using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL.Updates
{
    [TLObject(-304838614)]
    public class TLRequestGetState : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -304838614;
            }
        }

        public Updates.TLState Response { get; set; }

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
            Response = (Updates.TLState)ObjectUtils.DeserializeObject(br);
        }
    }
}
