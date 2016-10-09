using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-636267638)]
    public class TLChatParticipantCreator : TLAbsChatParticipant
    {

		
        public override int Constructor
        {
            get
            {
                return -636267638;
            }
        }

             public int user_id {get;set;}

		public TLChatParticipantCreator (){}
		public TLChatParticipantCreator (int user_id ){
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
