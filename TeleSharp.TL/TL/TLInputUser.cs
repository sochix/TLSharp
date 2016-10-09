using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-668391402)]
    public class TLInputUser : TLAbsInputUser
    {

		
        public override int Constructor
        {
            get
            {
                return -668391402;
            }
        }

             public int user_id {get;set;}
     public long access_hash {get;set;}

		public TLInputUser (int user_id ,long access_hash ){
			this.user_id = user_id; 
this.access_hash = access_hash; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            user_id = br.ReadInt32();
access_hash = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(user_id);
bw.Write(access_hash);

        }
    }
}
