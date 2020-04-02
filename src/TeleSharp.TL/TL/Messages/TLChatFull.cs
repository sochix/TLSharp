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

        public TLAbsChatFull FullChat { get; set; }
        public TLVector<TLAbsChat> Chats { get; set; }
        public TLVector<TLAbsUser> Users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            FullChat = (TLAbsChatFull)ObjectUtils.DeserializeObject(br);
            Chats = (TLVector<TLAbsChat>)ObjectUtils.DeserializeVector<TLAbsChat>(br);
            Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(FullChat, bw);
            ObjectUtils.SerializeObject(Chats, bw);
            ObjectUtils.SerializeObject(Users, bw);

        }
    }
}
