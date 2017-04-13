using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(364538944)]
    public class TLDialogs : TLAbsDialogs
    {
        public override int Constructor => 364538944;

        public TLVector<TLDialog> dialogs { get; set; }
        public TLVector<TLAbsMessage> messages { get; set; }
        public TLVector<TLAbsChat> chats { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            dialogs = ObjectUtils.DeserializeVector<TLDialog>(br);
            messages = ObjectUtils.DeserializeVector<TLAbsMessage>(br);
            chats = ObjectUtils.DeserializeVector<TLAbsChat>(br);
            users = ObjectUtils.DeserializeVector<TLAbsUser>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(dialogs, bw);
            ObjectUtils.SerializeObject(messages, bw);
            ObjectUtils.SerializeObject(chats, bw);
            ObjectUtils.SerializeObject(users, bw);
        }
    }
}