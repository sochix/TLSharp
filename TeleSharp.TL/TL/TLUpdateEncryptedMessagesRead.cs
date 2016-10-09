using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(956179895)]
    public class TLUpdateEncryptedMessagesRead : TLAbsUpdate
    {

		
        public override int Constructor
        {
            get
            {
                return 956179895;
            }
        }

             public int chat_id {get;set;}
     public int max_date {get;set;}
     public int date {get;set;}

		public TLUpdateEncryptedMessagesRead (){
			
		}
		public TLUpdateEncryptedMessagesRead (int chat_id ,int max_date ,int date ){
			this.chat_id = chat_id; 
this.max_date = max_date; 
this.date = date; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            chat_id = br.ReadInt32();
max_date = br.ReadInt32();
date = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(chat_id);
bw.Write(max_date);
bw.Write(date);

        }
    }
}
