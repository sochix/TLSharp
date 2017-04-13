using System.IO;

namespace TeleSharp.TL
{
    [TLObject(218751099)]
    public class TLInputPrivacyValueAllowContacts : TLAbsInputPrivacyRule
    {
        public override int Constructor => 218751099;


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