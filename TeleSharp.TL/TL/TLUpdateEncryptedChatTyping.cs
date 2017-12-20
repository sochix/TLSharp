using System.IO;

namespace TeleSharp.TL
{
    [TLObject(386986326)]
    public class TLUpdateEncryptedChatTyping : TLAbsUpdate
    {
        public int ChatId { get; set; }

        public override int Constructor
        {
            get
            {
                return 386986326;
            }
        }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            ChatId = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(ChatId);
        }
    }
}
