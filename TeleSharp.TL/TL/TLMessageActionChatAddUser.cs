using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1217033015)]
    public class TLMessageActionChatAddUser : TLAbsMessageAction
    {
        public override int Constructor => 1217033015;

        public TLVector<int> users { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            users = ObjectUtils.DeserializeVector<int>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(users, bw);
        }
    }
}