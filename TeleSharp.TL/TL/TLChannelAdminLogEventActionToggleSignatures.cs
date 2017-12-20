using System.IO;

namespace TeleSharp.TL
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
