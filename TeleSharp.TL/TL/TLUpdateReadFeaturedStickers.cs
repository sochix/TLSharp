using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1461528386)]
    public class TLUpdateReadFeaturedStickers : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 1461528386;
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