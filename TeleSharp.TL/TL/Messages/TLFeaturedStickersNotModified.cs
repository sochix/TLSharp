using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(82699215)]
    public class TLFeaturedStickersNotModified : TLAbsFeaturedStickers
    {
        public override int Constructor
        {
            get
            {
                return 82699215;
            }
        }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
        }
    }
}
