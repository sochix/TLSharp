using System.IO;

namespace TeleSharp.TL
{
    [TLObject(407582158)]
    public class TLInputPrivacyValueAllowAll : TLAbsInputPrivacyRule
    {
        public override int Constructor => 407582158;


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