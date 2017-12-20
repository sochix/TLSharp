using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-484987010)]
    public class TLUpdatesTooLong : TLAbsUpdates
    {
        public override int Constructor
        {
            get
            {
                return -484987010;
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
