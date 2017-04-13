using System.IO;
using TeleSharp.TL.Updates;

namespace TeleSharp.TL.Messages
{
    [TLObject(863093588)]
    public class TLPeerDialogs : TLObject
    {
        public override int Constructor => 863093588;

        public TLVector<TLDialog> dialogs { get; set; }
        public TLVector<TLAbsMessage> messages { get; set; }
        public TLVector<TLAbsChat> chats { get; set; }
        public TLVector<TLAbsUser> users { get; set; }
        public TLState state { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            dialogs = ObjectUtils.DeserializeVector<TLDialog>(br);
            messages = ObjectUtils.DeserializeVector<TLAbsMessage>(br);
            chats = ObjectUtils.DeserializeVector<TLAbsChat>(br);
            users = ObjectUtils.DeserializeVector<TLAbsUser>(br);
            state = (TLState) ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(dialogs, bw);
            ObjectUtils.SerializeObject(messages, bw);
            ObjectUtils.SerializeObject(chats, bw);
            ObjectUtils.SerializeObject(users, bw);
            ObjectUtils.SerializeObject(state, bw);
        }
    }
}