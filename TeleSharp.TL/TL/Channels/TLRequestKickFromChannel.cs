using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
	[TLObject(-1502421484)]
    public class TLRequestKickFromChannel : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1502421484;
            }
        }

                private TLAbsInputChannel channel {get;set;}
        private TLAbsInputUser user_id {get;set;}
        private bool kicked {get;set;}
        public TLAbsUpdates Response{ get; set;}

		
		public TLRequestKickFromChannel (TLAbsInputChannel channel ,TLAbsInputUser user_id ,bool kicked ){
			this.channel = channel; 
this.user_id = user_id; 
this.kicked = kicked; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
user_id = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
kicked = BoolUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel,bw);
ObjectUtils.SerializeObject(user_id,bw);
BoolUtil.Serialize(kicked,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

		}
    }
}
