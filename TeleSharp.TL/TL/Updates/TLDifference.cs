using System.IO;

namespace TeleSharp.TL.Updates
{
    [TLObject(16030880)]
    public class TLDifference : TLAbsDifference
    {
        public override int Constructor => 16030880;

        public TLVector<TLAbsMessage> new_messages { get; set; }
        public TLVector<TLAbsEncryptedMessage> new_encrypted_messages { get; set; }
        public TLVector<TLAbsUpdate> other_updates { get; set; }
        public TLVector<TLAbsChat> chats { get; set; }
        public TLVector<TLAbsUser> users { get; set; }
        public TLState state { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            new_messages = ObjectUtils.DeserializeVector<TLAbsMessage>(br);
            new_encrypted_messages = ObjectUtils.DeserializeVector<TLAbsEncryptedMessage>(br);
            other_updates = ObjectUtils.DeserializeVector<TLAbsUpdate>(br);
            chats = ObjectUtils.DeserializeVector<TLAbsChat>(br);
            users = ObjectUtils.DeserializeVector<TLAbsUser>(br);
            state = (TLState) ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(new_messages, bw);
            ObjectUtils.SerializeObject(new_encrypted_messages, bw);
            ObjectUtils.SerializeObject(other_updates, bw);
            ObjectUtils.SerializeObject(chats, bw);
            ObjectUtils.SerializeObject(users, bw);
            ObjectUtils.SerializeObject(state, bw);
        }
    }
}