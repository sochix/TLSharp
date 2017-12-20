using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-599948721)]
    public class TLTextEmpty : TLAbsRichText
    {
        public override int Constructor
        {
            get
            {
                return -599948721;
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
