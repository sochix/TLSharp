using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
	[TLObject(1231065863)]
    public class TLRequestToggleInvites : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1231065863;
            }
        }

                private TLAbsInputChannel channel {get;set;}
        private bool enabled {get;set;}
        public TLAbsUpdates Response{ get; set;}

		
		public TLRequestToggleInvites (TLAbsInputChannel channel ,bool enabled ){
			this.channel = channel; 
this.enabled = enabled; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
enabled = BoolUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel,bw);
BoolUtil.Serialize(enabled,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

		}
    }
}
