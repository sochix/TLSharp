using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-247351839)]
    public class TLInputEncryptedChat : TLObject
    {

		
        public override int Constructor
        {
            get
            {
                return -247351839;
            }
        }

             public int chat_id {get;set;}
     public long access_hash {get;set;}

		public TLInputEncryptedChat (int chat_id ,long access_hash ){
			this.chat_id = chat_id; 
this.access_hash = access_hash; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            chat_id = br.ReadInt32();
access_hash = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(chat_id);
bw.Write(access_hash);

        }
    }
}
