using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-352032773)]
    public class TLUpdateChannelTooLong : TLAbsUpdate
    {
        public int ChannelId { get; set; }

        public override int Constructor
        {
            get
            {
                return -352032773;
            }
        }

        public int Flags { get; set; }

        public int? Pts { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            ChannelId = br.ReadInt32();
            if ((Flags & 1) != 0)
                Pts = br.ReadInt32();
            else
                Pts = null;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            bw.Write(ChannelId);
            if ((Flags & 1) != 0)
                bw.Write(Pts.Value);
        }
    }
}
