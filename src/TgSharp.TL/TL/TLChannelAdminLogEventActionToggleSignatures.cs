using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(648939889)]
    public class TLChannelAdminLogEventActionToggleSignatures : TLAbsChannelAdminLogEventAction
    {
        public override int Constructor
        {
            get
            {
                return 648939889;
            }
        }

        public bool NewValue { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            NewValue = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BoolUtil.Serialize(NewValue, bw);
        }
    }
}
