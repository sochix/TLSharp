using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-1938715001)]
    public class TLMessages : TLAbsMessages
    {
        public override int Constructor => -1938715001;

        public TLVector<TLAbsMessage> messages { get; set; }
        public TLVector<TLAbsChat> chats { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            messages = ObjectUtils.DeserializeVector<TLAbsMessage>(br);
            chats = ObjectUtils.DeserializeVector<TLAbsChat>(br);
            users = ObjectUtils.DeserializeVector<TLAbsUser>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(messages, bw);
            ObjectUtils.SerializeObject(chats, bw);
            ObjectUtils.SerializeObject(users, bw);
        }
    }
}