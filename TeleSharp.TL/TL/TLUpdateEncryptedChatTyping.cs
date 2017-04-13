using System.IO;

namespace TeleSharp.TL
{
    [TLObject(386986326)]
    public class TLUpdateEncryptedChatTyping : TLAbsUpdate
    {
        public override int Constructor => 386986326;

        public int chat_id { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            chat_id = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(chat_id);
        }
    }
}