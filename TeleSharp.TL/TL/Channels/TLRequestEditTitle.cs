using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
	[TLObject(1450044624)]
    public class TLRequestEditTitle : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1450044624;
            }
        }

                private TLAbsInputChannel channel {get;set;}
        private string title {get;set;}
        public TLAbsUpdates Response{ get; set;}

		
		public TLRequestEditTitle (TLAbsInputChannel channel ,string title ){
			this.channel = channel; 
this.title = title; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
title = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel,bw);
StringUtil.Serialize(title,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

		}
    }
}
