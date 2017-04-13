using System.IO;

namespace TeleSharp.TL
{
    [TLObject(956179895)]
    public class TLUpdateEncryptedMessagesRead : TLAbsUpdate
    {
        public override int Constructor => 956179895;

        public int chat_id { get; set; }
        public int max_date { get; set; }
        public int date { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            chat_id = br.ReadInt32();
            max_date = br.ReadInt32();
            date = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(chat_id);
            bw.Write(max_date);
            bw.Write(date);
        }
    }
}