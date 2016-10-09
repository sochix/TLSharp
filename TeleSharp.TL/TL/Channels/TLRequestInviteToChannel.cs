using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
	[TLObject(429865580)]
    public class TLRequestInviteToChannel : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 429865580;
            }
        }

                private TLAbsInputChannel channel {get;set;}
        private TLVector<TLAbsInputUser> users {get;set;}
        public TLAbsUpdates Response{ get; set;}

		
		public TLRequestInviteToChannel (TLAbsInputChannel channel ,TLVector<TLAbsInputUser> users ){
			this.channel = channel; 
this.users = users; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
users = (TLVector<TLAbsInputUser>)ObjectUtils.DeserializeVector<TLAbsInputUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel,bw);
ObjectUtils.SerializeObject(users,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

		}
    }
}
