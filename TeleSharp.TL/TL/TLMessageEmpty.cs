using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-2082087340)]
    public class TLMessageEmpty : TLAbsMessage
    {
        public override int Constructor => -2082087340;

        public int id { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(id);
        }
    }
}