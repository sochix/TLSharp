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

        public long id { get; set; }
        public int date { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt64();
            date = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(id);
            bw.Write(date);
        }
    }
}