using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-317144808)]
    public class TLEncryptedMessage : TLAbsEncryptedMessage
    {

		
        public override int Constructor
        {
            get
            {
                return -317144808;
            }
        }

             public long random_id {get;set;}
     public int chat_id {get;set;}
     public int date {get;set;}
     public byte[] bytes {get;set;}
     public TLAbsEncryptedFile file {get;set;}

		public TLEncryptedMessage (){}
		public TLEncryptedMessage (long random_id ,int chat_id ,int date ,byte[] bytes ,TLAbsEncryptedFile file ){
			this.random_id = random_id; 
this.chat_id = chat_id; 
this.date = date; 
this.bytes = bytes; 
this.file = file; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            random_id = br.ReadInt64();
chat_id = br.ReadInt32();
date = br.ReadInt32();
bytes = BytesUtil.Deserialize(br);
file = (TLAbsEncryptedFile)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(random_id);
bw.Write(chat_id);
bw.Write(date);
BytesUtil.Serialize(bytes,bw);
ObjectUtils.SerializeObject(file,bw);

        }
    }
}
