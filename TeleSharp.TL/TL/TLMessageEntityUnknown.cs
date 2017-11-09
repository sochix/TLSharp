using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-1148011883)]
    public class TLMessageEntityUnknown : TLAbsMessageEntity
    {
        public override int Constructor
        {
            get
            {
                return -1148011883;
            }
        }

        public int offset { get; set; }
        public int length { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            offset = br.ReadInt32();
            length = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(offset);
            bw.Write(length);

        }
    }
}
