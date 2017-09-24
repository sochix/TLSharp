using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-618614392)]
    public class TLPageBlockDivider : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return -618614392;
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