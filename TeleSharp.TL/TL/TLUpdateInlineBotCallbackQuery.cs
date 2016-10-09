using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(750622127)]
    public class TLUpdateInlineBotCallbackQuery : TLAbsUpdate
    {

		
        public override int Constructor
        {
            get
            {
                return 750622127;
            }
        }

             public long query_id {get;set;}
     public int user_id {get;set;}
     public TLInputBotInlineMessageID msg_id {get;set;}
     public byte[] data {get;set;}

		public TLUpdateInlineBotCallbackQuery (){}
		public TLUpdateInlineBotCallbackQuery (long query_id ,int user_id ,TLInputBotInlineMessageID msg_id ,byte[] data ){
			this.query_id = query_id; 
this.user_id = user_id; 
this.msg_id = msg_id; 
this.data = data; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            query_id = br.ReadInt64();
user_id = br.ReadInt32();
msg_id = (TLInputBotInlineMessageID)ObjectUtils.DeserializeObject(br);
data = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(query_id);
bw.Write(user_id);
ObjectUtils.SerializeObject(msg_id,bw);
BytesUtil.Serialize(data,bw);

        }
    }
}
