using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1335282456)]
    public class TLInputPrivacyKeyStatusTimestamp : TLAbsInputPrivacyKey
    {
        public override int Constructor
        {
            get
            {
                return 1335282456;
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
