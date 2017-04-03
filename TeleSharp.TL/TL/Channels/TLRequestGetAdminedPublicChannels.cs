using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
	[TLObject(-1920105769)]
    public class TLRequestGetAdminedPublicChannels : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1920105769;
            }
        }

                public Messages.TLChats Response{ get; set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            
        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            
        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (Messages.TLChats)ObjectUtils.DeserializeObject(br);

		}
    }
}
