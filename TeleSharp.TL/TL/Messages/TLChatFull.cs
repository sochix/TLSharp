using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-438840932)]
    public class TLChatFull : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -438840932;
            }
        }

        public TLAbsChatFull full_chat { get; set; }
        public TLVector<TLAbsChat> chats { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            full_chat = (TLAbsChatFull)ObjectUtils.DeserializeObject(br);
            chats = (TLVector<TLAbsChat>)ObjectUtils.DeserializeVector<TLAbsChat>(br);
            users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(full_chat, bw);
            ObjectUtils.SerializeObject(chats, bw);
            ObjectUtils.SerializeObject(users, bw);

        }
    }
}
