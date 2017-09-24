using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-88417185)]
    public class TLInputPrivacyKeyPhoneCall : TLAbsInputPrivacyKey
    {
        public override int Constructor
        {
            get
            {
                return -88417185;
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