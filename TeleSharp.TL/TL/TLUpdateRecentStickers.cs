using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1706939360)]
    public class TLUpdateRecentStickers : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -1706939360;
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