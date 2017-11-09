using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-697604407)]
    public class TLInputPrivacyValueDisallowAll : TLAbsInputPrivacyRule
    {
        public override int Constructor
        {
            get
            {
                return -697604407;
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
