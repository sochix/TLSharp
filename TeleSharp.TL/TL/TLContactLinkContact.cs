using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-721239344)]
    public class TLContactLinkContact : TLAbsContactLink
    {
        public override int Constructor
        {
            get
            {
                return -721239344;
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
