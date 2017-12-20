using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1297179892)]
    public class TLMessageActionChatDeleteUser : TLAbsMessageAction
    {
        public override int Constructor
        {
            get
            {
                return -1297179892;
            }
        }

        public int UserId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            UserId = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(UserId);
        }
    }
}
