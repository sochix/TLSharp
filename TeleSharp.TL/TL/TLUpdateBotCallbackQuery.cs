using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-1500747636)]
    public class TLUpdateBotCallbackQuery : TLAbsUpdate
    {

		
        public override int Constructor
        {
            get
            {
                return -1500747636;
            }
        }

             public long query_id {get;set;}
     public int user_id {get;set;}
     public TLAbsPeer peer {get;set;}
     public int msg_id {get;set;}
     public byte[] data {get;set;}

		public TLUpdateBotCallbackQuery (){
			
		}
		public TLUpdateBotCallbackQuery (long query_id ,int user_id ,TLAbsPeer peer ,int msg_id ,byte[] data ){
			this.query_id = query_id; 
this.user_id = user_id; 
this.peer = peer; 
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
peer = (TLAbsPeer)ObjectUtils.DeserializeObject(br);
msg_id = br.ReadInt32();
data = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(query_id);
bw.Write(user_id);
ObjectUtils.SerializeObject(peer,bw);
bw.Write(msg_id);
BytesUtil.Serialize(data,bw);

        }
    }
}
