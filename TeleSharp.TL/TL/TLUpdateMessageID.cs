using System.IO;
namespace TeleSharp.TL
{
    [TLObject(1318109142)]
    public class TLUpdateMessageID : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 1318109142;
            }
        }

        public int id { get; set; }
        public long random_id { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt32();
            random_id = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(id);
            bw.Write(random_id);

        }
    }
}
