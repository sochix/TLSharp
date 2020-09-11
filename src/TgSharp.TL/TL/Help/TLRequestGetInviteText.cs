using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL.Help
{
    [TLObject(1295590211)]
    public class TLRequestGetInviteText : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1295590211;
            }
        }

        public Help.TLInviteText Response { get; set; }

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
            Response = (Help.TLInviteText)ObjectUtils.DeserializeObject(br);
        }
    }
}
