using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1015733815)]
    public class TLUpdateDeleteChannelMessages : TLAbsUpdate
    {
        public int ChannelId { get; set; }

        public override int Constructor
        {
            get
            {
                return -1015733815;
            }
        }

        public TLVector<int> Messages { get; set; }

        public int Pts { get; set; }

        public int PtsCount { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            ChannelId = br.ReadInt32();
            Messages = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);
            Pts = br.ReadInt32();
            PtsCount = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(ChannelId);
            ObjectUtils.SerializeObject(Messages, bw);
            bw.Write(Pts);
            bw.Write(PtsCount);
        }
    }
}
