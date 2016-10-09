using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-1297179892)]
    public class TLMessageActionChatDeleteUser : TLAbsMessageAction
    {

		
        public override int Constructor
        {
            get
            {
                return -1297179892;
            }
        }

             public int user_id {get;set;}

		public TLMessageActionChatDeleteUser (){
			
		}
		public TLMessageActionChatDeleteUser (int user_id ){
			this.user_id = user_id; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            user_id = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(user_id);

        }
    }
}
