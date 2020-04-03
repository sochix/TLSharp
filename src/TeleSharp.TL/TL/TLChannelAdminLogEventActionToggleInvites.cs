using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(460916654)]
    public class TLChannelAdminLogEventActionToggleInvites : TLAbsChannelAdminLogEventAction
    {
        public override int Constructor
        {
            get
            {
                return 460916654;
            }
        }

        public bool NewValue { get; set; }


        public void ComputeFlags()
        {

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
