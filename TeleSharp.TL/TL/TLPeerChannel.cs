using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1109531342)]
    public class TLPeerChannel : TLAbsPeer
    {
        public int ChannelId { get; set; }

        public override int Constructor
        {
            get
            {
                return -1109531342;
            }
        }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            ChannelId = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(ChannelId);
        }
    }
}
