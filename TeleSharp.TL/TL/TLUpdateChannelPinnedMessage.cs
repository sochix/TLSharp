using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1738988427)]
    public class TLUpdateChannelPinnedMessage : TLAbsUpdate
    {
        public int ChannelId { get; set; }

        public override int Constructor
        {
            get
            {
                return -1738988427;
            }
        }

        public int Id { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            ChannelId = br.ReadInt32();
            Id = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(ChannelId);
            bw.Write(Id);
        }
    }
}
