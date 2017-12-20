using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1318109142)]
    public class TLUpdateMessageID : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 1318109142;
            }
        }

        public int Id { get; set; }

        public long RandomId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = br.ReadInt32();
            RandomId = br.ReadInt64();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Id);
            bw.Write(RandomId);
        }
    }
}
