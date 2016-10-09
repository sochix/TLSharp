using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(852769188)]
    public class TLRequestSendEncryptedService : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 852769188;
            }
        }

                private TLInputEncryptedChat peer {get;set;}
        private long random_id {get;set;}
        private byte[] data {get;set;}
        public Messages.TLAbsSentEncryptedMessage Response{ get; set;}

		
		public TLRequestSendEncryptedService (TLInputEncryptedChat peer ,long random_id ,byte[] data ){
			this.peer = peer; 
this.random_id = random_id; 
this.data = data; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLInputEncryptedChat)ObjectUtils.DeserializeObject(br);
random_id = br.ReadInt64();
data = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer,bw);
bw.Write(random_id);
BytesUtil.Serialize(data,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (Messages.TLAbsSentEncryptedMessage)ObjectUtils.DeserializeObject(br);

		}
    }
}
