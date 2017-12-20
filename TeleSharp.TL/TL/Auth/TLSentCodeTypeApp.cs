using System.IO;

namespace TeleSharp.TL.Auth
{
    [TLObject(1035688326)]
    public class TLSentCodeTypeApp : TLAbsSentCodeType
    {
        public override int Constructor
        {
            get
            {
                return 1035688326;
            }
        }

        public int Length { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Length = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Length);
        }
    }
}
