using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1427671598)]
    public class TLChannelAdminLogEventActionChangeAbout : TLAbsChannelAdminLogEventAction
    {
        public override int Constructor
        {
            get
            {
                return 1427671598;
            }
        }

        public string NewValue { get; set; }

        public string PrevValue { get; set; }

        public void ComputeFlags()
        {
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
