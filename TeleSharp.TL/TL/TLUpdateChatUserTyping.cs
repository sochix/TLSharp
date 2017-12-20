using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1704596961)]
    public class TLUpdateChatUserTyping : TLAbsUpdate
    {
        public TLAbsSendMessageAction Action { get; set; }

        public int ChatId { get; set; }

        public override int Constructor
        {
            get
            {
                return -1704596961;
            }
        }

        public int UserId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            ChatId = br.ReadInt32();
            UserId = br.ReadInt32();
            Action = (TLAbsSendMessageAction)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(ChatId);
            bw.Write(UserId);
            ObjectUtils.SerializeObject(Action, bw);
        }
    }
}
