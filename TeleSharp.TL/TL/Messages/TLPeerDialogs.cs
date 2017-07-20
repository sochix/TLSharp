using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(863093588)]
    public class TLPeerDialogs : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 863093588;
            }
        }

        public TLVector<TLDialog> dialogs { get; set; }
        public TLVector<TLAbsMessage> messages { get; set; }
        public TLVector<TLAbsChat> chats { get; set; }
        public TLVector<TLAbsUser> users { get; set; }
        public Updates.TLState state { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            dialogs = (TLVector<TLDialog>)ObjectUtils.DeserializeVector<TLDialog>(br);
            messages = (TLVector<TLAbsMessage>)ObjectUtils.DeserializeVector<TLAbsMessage>(br);
            chats = (TLVector<TLAbsChat>)ObjectUtils.DeserializeVector<TLAbsChat>(br);
            users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);
            state = (Updates.TLState)ObjectUtils.DeserializeObject(br);

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
