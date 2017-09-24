using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-395694988)]
    public class TLInputPeerNotifyEventsAll : TLAbsInputPeerNotifyEvents
    {
        public override int Constructor
        {
            get
            {
                return -395694988;
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