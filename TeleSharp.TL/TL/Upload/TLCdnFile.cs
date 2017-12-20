using System.IO;

namespace TeleSharp.TL.Upload
{
    [TLObject(-1449145777)]
    public class TLCdnFile : TLAbsCdnFile
    {
        public byte[] Bytes { get; set; }

        public override int Constructor
        {
            get
            {
                return -1449145777;
            }
        }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Bytes = BytesUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(Bytes, bw);
        }
    }
}
