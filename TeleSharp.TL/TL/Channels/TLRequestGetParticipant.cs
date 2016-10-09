using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
	[TLObject(1416484774)]
    public class TLRequestGetParticipant : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1416484774;
            }
        }

                private TLAbsInputChannel channel {get;set;}
        private TLAbsInputUser user_id {get;set;}
        public Channels.TLChannelParticipant Response{ get; set;}

		
		public TLRequestGetParticipant (TLAbsInputChannel channel ,TLAbsInputUser user_id ){
			this.channel = channel; 
this.user_id = user_id; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
user_id = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel,bw);
ObjectUtils.SerializeObject(user_id,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (Channels.TLChannelParticipant)ObjectUtils.DeserializeObject(br);

		}
    }
}
