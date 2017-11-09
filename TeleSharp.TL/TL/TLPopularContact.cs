using System.IO;
namespace TeleSharp.TL
{
    [TLObject(1558266229)]
    public class TLPopularContact : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1558266229;
            }
        }

        public long client_id { get; set; }
        public int importers { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            client_id = br.ReadInt64();
            importers = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(client_id);
            bw.Write(importers);

        }
    }
}
