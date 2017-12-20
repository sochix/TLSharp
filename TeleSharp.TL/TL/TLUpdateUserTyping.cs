using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1548249383)]
    public class TLUpdateUserTyping : TLAbsUpdate
    {
        public TLAbsSendMessageAction Action { get; set; }

        public override int Constructor
        {
            get
            {
                return 1548249383;
            }
        }

        public int UserId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            UserId = br.ReadInt32();
            Action = (TLAbsSendMessageAction)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(UserId);
            ObjectUtils.SerializeObject(Action, bw);
        }
    }
}
