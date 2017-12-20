using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1558266229)]
    public class TLPopularContact : TLObject
    {
        public long ClientId { get; set; }

        public override int Constructor
        {
            get
            {
                return 1558266229;
            }
        }

        public int Importers { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            ClientId = br.ReadInt64();
            Importers = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(ClientId);
            bw.Write(Importers);
        }
    }
}
