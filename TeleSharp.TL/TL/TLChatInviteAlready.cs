using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1516793212)]
    public class TLChatInviteAlready : TLAbsChatInvite
    {
        public TLAbsChat Chat { get; set; }

        public override int Constructor
        {
            get
            {
                return 1516793212;
            }
        }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Chat = (TLAbsChat)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Chat, bw);
        }
    }
}
