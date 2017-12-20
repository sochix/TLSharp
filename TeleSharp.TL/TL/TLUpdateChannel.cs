using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1227598250)]
    public class TLUpdateChannel : TLAbsUpdate
    {
        public int ChannelId { get; set; }

        public override int Constructor
        {
            get
            {
                return -1227598250;
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
