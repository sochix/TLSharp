using System.IO;

namespace TeleSharp.TL
{
    [TLObject(634833351)]
    public class TLUpdateReadChannelOutbox : TLAbsUpdate
    {
        public override int Constructor => 634833351;

        public int channel_id { get; set; }
        public int max_id { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            channel_id = br.ReadInt32();
            max_id = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(channel_id);
            bw.Write(max_id);
        }
    }
}