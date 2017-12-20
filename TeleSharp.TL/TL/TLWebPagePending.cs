using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-981018084)]
    public class TLWebPagePending : TLAbsWebPage
    {
        public override int Constructor
        {
            get
            {
                return -981018084;
            }
        }

        public int Date { get; set; }

        public long Id { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = br.ReadInt64();
            Date = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Id);
            bw.Write(Date);
        }
    }
}
