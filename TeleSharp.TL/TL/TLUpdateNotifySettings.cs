using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1094555409)]
    public class TLUpdateNotifySettings : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -1094555409;
            }
        }

        public TLAbsPeerNotifySettings NotifySettings { get; set; }

        public TLAbsNotifyPeer Peer { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Peer = (TLAbsNotifyPeer)ObjectUtils.DeserializeObject(br);
            NotifySettings = (TLAbsPeerNotifySettings)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Peer, bw);
            ObjectUtils.SerializeObject(NotifySettings, bw);
        }
    }
}
