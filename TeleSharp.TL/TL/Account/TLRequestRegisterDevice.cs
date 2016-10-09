using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
	[TLObject(1669245048)]
    public class TLRequestRegisterDevice : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1669245048;
            }
        }

                private int token_type {get;set;}
        private string token {get;set;}
        public bool Response{ get; set;}

		
		public TLRequestRegisterDevice (int token_type ,string token ){
			this.token_type = token_type; 
this.token = token; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            token_type = br.ReadInt32();
token = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(token_type);
StringUtil.Serialize(token,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = BoolUtil.Deserialize(br);

		}
    }
}
