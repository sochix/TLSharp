using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1859134776)]
    public class TLMessageEntityUrl : TLAbsMessageEntity
    {
        public override int Constructor
        {
            get
            {
                return 1859134776;
            }
        }

        public int Length { get; set; }

        public int Offset { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Offset = br.ReadInt32();
            Length = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Offset);
            bw.Write(Length);
        }
    }
}
