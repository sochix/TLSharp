using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-805141448)]
    public class TLImportedContact : TLObject
    {

		
        public override int Constructor
        {
            get
            {
                return -805141448;
            }
        }

             public int user_id {get;set;}
     public long client_id {get;set;}

		public TLImportedContact (int user_id ,long client_id ){
			this.user_id = user_id; 
this.client_id = client_id; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            user_id = br.ReadInt32();
client_id = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(user_id);
bw.Write(client_id);

        }
    }
}
