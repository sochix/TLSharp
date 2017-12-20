using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-1058912715)]
    public class TLDhConfigNotModified : TLAbsDhConfig
    {
        public override int Constructor
        {
            get
            {
                return -1058912715;
            }
        }

        public byte[] Random { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Random = BytesUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(Random, bw);
        }
    }
}
