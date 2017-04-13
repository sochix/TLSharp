using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(189033187)]
    public class TLMessagesSlice : TLAbsMessages
    {
        public override int Constructor => 189033187;

        public int count { get; set; }
        public TLVector<TLAbsMessage> messages { get; set; }
        public TLVector<TLAbsChat> chats { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            count = br.ReadInt32();
            messages = ObjectUtils.DeserializeVector<TLAbsMessage>(br);
            chats = ObjectUtils.DeserializeVector<TLAbsChat>(br);
            users = ObjectUtils.DeserializeVector<TLAbsUser>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(count);
            ObjectUtils.SerializeObject(messages, bw);
            ObjectUtils.SerializeObject(chats, bw);
            ObjectUtils.SerializeObject(users, bw);
        }
    }
}