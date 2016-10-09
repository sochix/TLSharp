using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-94974410)]
    public class TLEncryptedChat : TLAbsEncryptedChat
    {

		
        public override int Constructor
        {
            get
            {
                return -94974410;
            }
        }

             public int id {get;set;}
     public long access_hash {get;set;}
     public int date {get;set;}
     public int admin_id {get;set;}
     public int participant_id {get;set;}
     public byte[] g_a_or_b {get;set;}
     public long key_fingerprint {get;set;}

		public TLEncryptedChat (){}
		public TLEncryptedChat (int id ,long access_hash ,int date ,int admin_id ,int participant_id ,byte[] g_a_or_b ,long key_fingerprint ){
			this.id = id; 
this.access_hash = access_hash; 
this.date = date; 
this.admin_id = admin_id; 
this.participant_id = participant_id; 
this.g_a_or_b = g_a_or_b; 
this.key_fingerprint = key_fingerprint; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt32();
access_hash = br.ReadInt64();
date = br.ReadInt32();
admin_id = br.ReadInt32();
participant_id = br.ReadInt32();
g_a_or_b = BytesUtil.Deserialize(br);
key_fingerprint = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(id);
bw.Write(access_hash);
bw.Write(date);
bw.Write(admin_id);
bw.Write(participant_id);
BytesUtil.Serialize(g_a_or_b,bw);
bw.Write(key_fingerprint);

        }
    }
}
