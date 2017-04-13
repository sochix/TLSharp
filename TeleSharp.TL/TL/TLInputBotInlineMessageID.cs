using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1995686519)]
    public class TLInputBotInlineMessageID : TLObject
    {
        public override int Constructor => -1995686519;

        public int dc_id { get; set; }
        public long id { get; set; }
        public long access_hash { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            dc_id = br.ReadInt32();
            id = br.ReadInt64();
            access_hash = br.ReadInt64();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(dc_id);
            bw.Write(id);
            bw.Write(access_hash);
        }
    }
}