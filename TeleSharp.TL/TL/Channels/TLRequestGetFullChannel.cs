using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
	[TLObject(141781513)]
    public class TLRequestGetFullChannel : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 141781513;
            }
        }

                public TLAbsInputChannel channel {get;set;}
        public Messages.TLChatFull Response{ get; set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (Messages.TLChatFull)ObjectUtils.DeserializeObject(br);

		}
    }
}
