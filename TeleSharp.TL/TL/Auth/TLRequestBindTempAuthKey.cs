using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
	[TLObject(-841733627)]
    public class TLRequestBindTempAuthKey : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -841733627;
            }
        }

                private long perm_auth_key_id {get;set;}
        private long nonce {get;set;}
        private int expires_at {get;set;}
        private byte[] encrypted_message {get;set;}
        public bool Response{ get; set;}

		
		public TLRequestBindTempAuthKey (long perm_auth_key_id ,long nonce ,int expires_at ,byte[] encrypted_message ){
			this.perm_auth_key_id = perm_auth_key_id; 
this.nonce = nonce; 
this.expires_at = expires_at; 
this.encrypted_message = encrypted_message; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            perm_auth_key_id = br.ReadInt64();
nonce = br.ReadInt64();
expires_at = br.ReadInt32();
encrypted_message = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(perm_auth_key_id);
bw.Write(nonce);
bw.Write(expires_at);
BytesUtil.Serialize(encrypted_message,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = BoolUtil.Deserialize(br);

		}
    }
}
