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

        public bool new_value { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            new_value = BoolUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BoolUtil.Serialize(new_value, bw);

        }
    }
}
