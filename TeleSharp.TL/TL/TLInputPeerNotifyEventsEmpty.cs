using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-265263912)]
    public class TLInputPeerNotifyEventsEmpty : TLAbsInputPeerNotifyEvents
    {
        public override int Constructor
        {
            get
            {
                return -265263912;
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
