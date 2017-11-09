using System.IO;
namespace TeleSharp.TL
{
    [TLObject(1783299128)]
    public class TLChannelAdminLogEventActionChangeUsername : TLAbsChannelAdminLogEventAction
    {
        public override int Constructor
        {
            get
            {
                return 1783299128;
            }
        }

        public string prev_value { get; set; }
        public string new_value { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            prev_value = StringUtil.Deserialize(br);
            new_value = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(prev_value, bw);
            StringUtil.Serialize(new_value, bw);

        }
    }
}
