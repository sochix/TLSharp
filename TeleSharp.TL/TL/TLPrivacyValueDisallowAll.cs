using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-1955338397)]
    public class TLPrivacyValueDisallowAll : TLAbsPrivacyRule
    {
        public override int Constructor
        {
            get
            {
                return -1955338397;
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
