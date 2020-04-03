using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1599903217)]
    public class TLChannelAdminLogEventActionTogglePreHistoryHidden : TLAbsChannelAdminLogEventAction
    {
        public override int Constructor
        {
            get
            {
                return 1599903217;
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
