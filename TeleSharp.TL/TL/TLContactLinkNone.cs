using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-17968211)]
    public class TLContactLinkNone : TLAbsContactLink
    {
        public override int Constructor
        {
            get
            {
                return -17968211;
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
