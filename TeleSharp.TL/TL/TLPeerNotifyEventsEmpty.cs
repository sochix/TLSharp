using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1378534221)]
    public class TLPeerNotifyEventsEmpty : TLAbsPeerNotifyEvents
    {
        public override int Constructor => -1378534221;


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