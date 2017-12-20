using System.IO;

namespace TeleSharp.TL
{
    [TLObject(511092620)]
    public class TLTopPeerCategoryPhoneCalls : TLAbsTopPeerCategory
    {
        public override int Constructor
        {
            get
            {
                return 511092620;
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
