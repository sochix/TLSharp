using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-805141448)]
    public class TLImportedContact : TLObject
    {
        public override int Constructor => -805141448;

        public int user_id { get; set; }
        public long client_id { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            user_id = br.ReadInt32();
            client_id = br.ReadInt64();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(user_id);
            bw.Write(client_id);
        }
    }
}