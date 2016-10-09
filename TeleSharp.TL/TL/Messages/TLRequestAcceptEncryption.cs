using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(1035731989)]
    public class TLRequestAcceptEncryption : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1035731989;
            }
        }

                private TLInputEncryptedChat peer {get;set;}
        private byte[] g_b {get;set;}
        private long key_fingerprint {get;set;}
        public TLAbsEncryptedChat Response{ get; set;}

		
		public TLRequestAcceptEncryption (TLInputEncryptedChat peer ,byte[] g_b ,long key_fingerprint ){
			this.peer = peer; 
this.g_b = g_b; 
this.key_fingerprint = key_fingerprint; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLInputEncryptedChat)ObjectUtils.DeserializeObject(br);
g_b = BytesUtil.Deserialize(br);
key_fingerprint = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer,bw);
BytesUtil.Serialize(g_b,bw);
bw.Write(key_fingerprint);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (TLAbsEncryptedChat)ObjectUtils.DeserializeObject(br);

		}
    }
}
