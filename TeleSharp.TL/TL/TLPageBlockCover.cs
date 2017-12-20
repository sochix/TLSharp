using System.IO;

namespace TeleSharp.TL
{
    [TLObject(972174080)]
    public class TLPageBlockCover : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return 972174080;
            }
        }

        public TLAbsPageBlock Cover { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Cover = (TLAbsPageBlock)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Cover, bw);
        }
    }
}
