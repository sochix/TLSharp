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

        public TLAbsInputPeer peer { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
        }
    }
}