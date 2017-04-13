using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-123988)]
    public class TLPrivacyValueAllowContacts : TLAbsPrivacyRule
    {
        public override int Constructor => -123988;


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