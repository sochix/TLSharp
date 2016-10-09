using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
	[TLObject(-248621111)]
    public class TLRequestEditPhoto : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -248621111;
            }
        }

                private TLAbsInputChannel channel {get;set;}
        private TLAbsInputChatPhoto photo {get;set;}
        public TLAbsUpdates Response{ get; set;}

		
		public TLRequestEditPhoto (TLAbsInputChannel channel ,TLAbsInputChatPhoto photo ){
			this.channel = channel; 
this.photo = photo; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
photo = (TLAbsInputChatPhoto)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel,bw);
ObjectUtils.SerializeObject(photo,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

		}
    }
}
