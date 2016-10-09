using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
	[TLObject(-32999408)]
    public class TLRequestReportSpam : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -32999408;
            }
        }

                private TLAbsInputChannel channel {get;set;}
        private TLAbsInputUser user_id {get;set;}
        private TLVector<int> id {get;set;}
        public bool Response{ get; set;}

		
		public TLRequestReportSpam (TLAbsInputChannel channel ,TLAbsInputUser user_id ,TLVector<int> id ){
			this.channel = channel; 
this.user_id = user_id; 
this.id = id; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
user_id = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
id = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel,bw);
ObjectUtils.SerializeObject(user_id,bw);
ObjectUtils.SerializeObject(id,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = BoolUtil.Deserialize(br);

		}
    }
}
