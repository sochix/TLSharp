using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(1051570619)]
    public class TLRequestCheckChatInvite : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1051570619;
            }
        }

                private string hash {get;set;}
        public TLAbsChatInvite Response{ get; set;}

		
		public TLRequestCheckChatInvite (string hash ){
			this.hash = hash; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            hash = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            StringUtil.Serialize(hash,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (TLAbsChatInvite)ObjectUtils.DeserializeObject(br);

		}
    }
}
