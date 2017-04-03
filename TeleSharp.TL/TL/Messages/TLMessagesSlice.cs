using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(189033187)]
    public class TLMessagesSlice : TLAbsMessages
    {
        public override int Constructor
        {
            get
            {
                return 189033187;
            }
        }

             public int count {get;set;}
     public TLVector<TLAbsMessage> messages {get;set;}
     public TLVector<TLAbsChat> chats {get;set;}
     public TLVector<TLAbsUser> users {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            count = br.ReadInt32();
messages = (TLVector<TLAbsMessage>)ObjectUtils.DeserializeVector<TLAbsMessage>(br);
chats = (TLVector<TLAbsChat>)ObjectUtils.DeserializeVector<TLAbsChat>(br);
users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(count);
ObjectUtils.SerializeObject(messages,bw);
ObjectUtils.SerializeObject(chats,bw);
ObjectUtils.SerializeObject(users,bw);

        }
    }
}
