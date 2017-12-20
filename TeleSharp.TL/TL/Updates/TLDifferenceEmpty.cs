using System.IO;

namespace TeleSharp.TL.Updates
{
    [TLObject(1567990072)]
    public class TLDifferenceEmpty : TLAbsDifference
    {
        public override int Constructor
        {
            get
            {
                return 1567990072;
            }
        }

        public int Date { get; set; }

        public int Seq { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Date = br.ReadInt32();
            Seq = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Date);
            bw.Write(Seq);
        }
    }
}
