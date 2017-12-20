using System.IO;

namespace TeleSharp.TL.Updates
{
    [TLObject(16030880)]
    public class TLDifference : TLAbsDifference
    {
        public TLVector<TLAbsChat> Chats { get; set; }

        public override int Constructor
        {
            get
            {
                return 16030880;
            }
        }

        public TLVector<TLAbsEncryptedMessage> NewEncryptedMessages { get; set; }

        public TLVector<TLAbsMessage> NewMessages { get; set; }

        public TLVector<TLAbsUpdate> OtherUpdates { get; set; }

        public Updates.TLState State { get; set; }

        public TLVector<TLAbsUser> Users { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            NewMessages = (TLVector<TLAbsMessage>)ObjectUtils.DeserializeVector<TLAbsMessage>(br);
            NewEncryptedMessages = (TLVector<TLAbsEncryptedMessage>)ObjectUtils.DeserializeVector<TLAbsEncryptedMessage>(br);
            OtherUpdates = (TLVector<TLAbsUpdate>)ObjectUtils.DeserializeVector<TLAbsUpdate>(br);
            Chats = (TLVector<TLAbsChat>)ObjectUtils.DeserializeVector<TLAbsChat>(br);
            Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);
            State = (Updates.TLState)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(NewMessages, bw);
            ObjectUtils.SerializeObject(NewEncryptedMessages, bw);
            ObjectUtils.SerializeObject(OtherUpdates, bw);
            ObjectUtils.SerializeObject(Chats, bw);
            ObjectUtils.SerializeObject(Users, bw);
            ObjectUtils.SerializeObject(State, bw);
        }
    }
}
