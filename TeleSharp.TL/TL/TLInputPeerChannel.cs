using System.IO;

namespace TeleSharp.TL
{
    [TLObject(548253432)]
    public class TLInputPeerChannel : TLAbsInputPeer
    {
        public long AccessHash { get; set; }

        public int ChannelId { get; set; }

        public override int Constructor
        {
            get
            {
                return 548253432;
            }
        }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            ChannelId = br.ReadInt32();
            AccessHash = br.ReadInt64();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(ChannelId);
            bw.Write(AccessHash);
        }
    }
}
