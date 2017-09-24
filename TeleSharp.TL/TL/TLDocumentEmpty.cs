using System.IO;

namespace TeleSharp.TL
{
    [TLObject(922273905)]
    public class TLDocumentEmpty : TLAbsDocument
    {
        public override int Constructor
        {
            get
            {
                return 922273905;
            }
        }

        public long id { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt64();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(id);
        }
    }
}