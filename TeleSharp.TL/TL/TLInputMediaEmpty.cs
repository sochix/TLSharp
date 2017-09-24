using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1771768449)]
    public class TLInputMediaEmpty : TLAbsInputMedia
    {
        public override int Constructor
        {
            get
            {
                return -1771768449;
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