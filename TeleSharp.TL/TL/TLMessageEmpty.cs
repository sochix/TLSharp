using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-2082087340)]
    public class TLMessageEmpty : TLAbsMessage
    {
        public override int Constructor
        {
            get
            {
                return -2082087340;
            }
        }

        public int Id { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Id);
        }
    }
}
