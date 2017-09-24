using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1959820384)]
    public class TLNotifyAll : TLAbsNotifyPeer
    {
        public override int Constructor
        {
            get
            {
                return 1959820384;
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