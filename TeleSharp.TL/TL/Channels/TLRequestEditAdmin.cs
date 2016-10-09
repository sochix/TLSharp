using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
	[TLObject(-344583728)]
    public class TLRequestEditAdmin : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -344583728;
            }
        }

                private TLAbsInputChannel channel {get;set;}
        private TLAbsInputUser user_id {get;set;}
        private TLAbsChannelParticipantRole role {get;set;}
        public TLAbsUpdates Response{ get; set;}

		
		public TLRequestEditAdmin (TLAbsInputChannel channel ,TLAbsInputUser user_id ,TLAbsChannelParticipantRole role ){
			this.channel = channel; 
this.user_id = user_id; 
this.role = role; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
user_id = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
role = (TLAbsChannelParticipantRole)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel,bw);
ObjectUtils.SerializeObject(user_id,bw);
ObjectUtils.SerializeObject(role,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

		}
    }
}
