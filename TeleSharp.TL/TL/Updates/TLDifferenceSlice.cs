using System.IO;

namespace TeleSharp.TL.Updates
{
    [TLObject(-1459938943)]
    public class TLDifferenceSlice : TLAbsDifference
    {
        public TLVector<TLAbsChat> Chats { get; set; }

        public override int Constructor
        {
            get
            {
                return -1459938943;
            }
        }

        public Updates.TLState IntermediateState { get; set; }

        public TLVector<TLAbsEncryptedMessage> NewEncryptedMessages { get; set; }

        public TLVector<TLAbsMessage> NewMessages { get; set; }

        public TLVector<TLAbsUpdate> OtherUpdates { get; set; }

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
            IntermediateState = (Updates.TLState)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(NewMessages, bw);
            ObjectUtils.SerializeObject(NewEncryptedMessages, bw);
            ObjectUtils.SerializeObject(OtherUpdates, bw);
            ObjectUtils.SerializeObject(Chats, bw);
            ObjectUtils.SerializeObject(Users, bw);
            ObjectUtils.SerializeObject(IntermediateState, bw);
        }
    }
}
