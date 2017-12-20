using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1030105979)]
    public class TLPrivacyKeyPhoneCall : TLAbsPrivacyKey
    {
        public override int Constructor
        {
            get
            {
                return 1030105979;
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
