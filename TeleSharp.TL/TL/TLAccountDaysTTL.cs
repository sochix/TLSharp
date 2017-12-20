using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1194283041)]
    public class TLAccountDaysTTL : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1194283041;
            }
        }

        public int Days { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Days = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Days);
        }
    }
}
