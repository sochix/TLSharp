using System.IO;

namespace TeleSharp.TL
{
    [TLObject(371037736)]
    public class TLTopPeerCategoryChannels : TLAbsTopPeerCategory
    {
        public override int Constructor
        {
            get
            {
                return 371037736;
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
