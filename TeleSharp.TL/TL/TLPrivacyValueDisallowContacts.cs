using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-125240806)]
    public class TLPrivacyValueDisallowContacts : TLAbsPrivacyRule
    {
        public override int Constructor
        {
            get
            {
                return -125240806;
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