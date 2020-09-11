using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL.Help
{
    [TLObject(531836966)]
    public class TLRequestGetNearestDc : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 531836966;
            }
        }

        public TLNearestDc Response { get; set; }

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
            Response = (TLNearestDc)ObjectUtils.DeserializeObject(br);
        }
    }
}
