using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1195615476)]
    public class TLInputNotifyPeer : TLAbsInputNotifyPeer
    {
        public override int Constructor
        {
            get
            {
                return -1195615476;
            }
        }

        public TLAbsInputPeer Peer { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Peer, bw);
        }
    }
}
