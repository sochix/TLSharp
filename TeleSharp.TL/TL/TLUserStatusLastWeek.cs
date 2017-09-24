using System.IO;

namespace TeleSharp.TL
{
    [TLObject(129960444)]
    public class TLUserStatusLastWeek : TLAbsUserStatus
    {
        public override int Constructor
        {
            get
            {
                return 129960444;
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