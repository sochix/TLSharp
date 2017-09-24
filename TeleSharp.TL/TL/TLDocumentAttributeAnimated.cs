using System.IO;

namespace TeleSharp.TL
{
    [TLObject(297109817)]
    public class TLDocumentAttributeAnimated : TLAbsDocumentAttribute
    {
        public override int Constructor
        {
            get
            {
                return 297109817;
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