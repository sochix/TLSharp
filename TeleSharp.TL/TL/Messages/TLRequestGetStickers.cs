using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(-1373446075)]
    public class TLRequestGetStickers : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1373446075;
            }
        }

                private string emoticon {get;set;}
        private string hash {get;set;}
        public Messages.TLAbsStickers Response{ get; set;}

		
		public TLRequestGetStickers (string emoticon ,string hash ){
			this.emoticon = emoticon; 
this.hash = hash; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            emoticon = StringUtil.Deserialize(br);
hash = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            StringUtil.Serialize(emoticon,bw);
StringUtil.Serialize(hash,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (Messages.TLAbsStickers)ObjectUtils.DeserializeObject(br);

		}
    }
}
