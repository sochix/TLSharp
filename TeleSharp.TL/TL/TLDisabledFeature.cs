using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1369215196)]
    public class TLDisabledFeature : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1369215196;
            }
        }

        public string Description { get; set; }

        public string Feature { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Feature = StringUtil.Deserialize(br);
            Description = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Feature, bw);
            StringUtil.Serialize(Description, bw);
        }
    }
}
