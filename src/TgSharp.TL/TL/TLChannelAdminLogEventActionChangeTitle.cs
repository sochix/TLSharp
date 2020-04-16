using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(-421545947)]
    public class TLChannelAdminLogEventActionChangeTitle : TLAbsChannelAdminLogEventAction
    {
        public override int Constructor
        {
            get
            {
                return -421545947;
            }
        }

        public string PrevValue { get; set; }
        public string NewValue { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            PrevValue = StringUtil.Deserialize(br);
            NewValue = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(PrevValue, bw);
            StringUtil.Serialize(NewValue, bw);
        }
    }
}
