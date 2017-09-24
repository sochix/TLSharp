using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-496024847)]
    public class TLUserStatusRecently : TLAbsUserStatus
    {
        public override int Constructor
        {
            get
            {
                return -496024847;
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