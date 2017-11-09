using System.IO;
namespace TeleSharp.TL
{
    [TLObject(2012136335)]
    public class TLCdnFileHash : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 2012136335;
            }
        }

        public int offset { get; set; }
        public int limit { get; set; }
        public byte[] hash { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            offset = br.ReadInt32();
            limit = br.ReadInt32();
            hash = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(offset);
            bw.Write(limit);
            BytesUtil.Serialize(hash, bw);

        }
    }
}
