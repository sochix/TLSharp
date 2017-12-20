using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1830677896)]
    public class TLPeerNotifyEventsAll : TLAbsPeerNotifyEvents
    {
        public override int Constructor
        {
            get
            {
                return 1830677896;
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
