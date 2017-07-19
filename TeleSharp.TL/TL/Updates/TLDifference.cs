using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Updates
{
    [TLObject(16030880)]
    public class TLDifference : TLAbsDifference
    {
        public override int Constructor
        {
            get
            {
                return 16030880;
            }
        }

        public TLVector<TLAbsMessage> new_messages { get; set; }
        public TLVector<TLAbsEncryptedMessage> new_encrypted_messages { get; set; }
        public TLVector<TLAbsUpdate> other_updates { get; set; }
        public TLVector<TLAbsChat> chats { get; set; }
        public TLVector<TLAbsUser> users { get; set; }
        public Updates.TLState state { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            new_messages = (TLVector<TLAbsMessage>)ObjectUtils.DeserializeVector<TLAbsMessage>(br);
            new_encrypted_messages = (TLVector<TLAbsEncryptedMessage>)ObjectUtils.DeserializeVector<TLAbsEncryptedMessage>(br);
            other_updates = (TLVector<TLAbsUpdate>)ObjectUtils.DeserializeVector<TLAbsUpdate>(br);
            chats = (TLVector<TLAbsChat>)ObjectUtils.DeserializeVector<TLAbsChat>(br);
            users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);
            state = (Updates.TLState)ObjectUtils.DeserializeObject(br);

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
