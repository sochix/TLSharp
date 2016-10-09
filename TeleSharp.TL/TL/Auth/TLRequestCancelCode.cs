using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
	[TLObject(520357240)]
    public class TLRequestCancelCode : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 520357240;
            }
        }

                private string phone_number {get;set;}
        private string phone_code_hash {get;set;}
        public bool Response{ get; set;}

		
		public TLRequestCancelCode (string phone_number ,string phone_code_hash ){
			this.phone_number = phone_number; 
this.phone_code_hash = phone_code_hash; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            phone_number = StringUtil.Deserialize(br);
phone_code_hash = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            StringUtil.Serialize(phone_number,bw);
StringUtil.Serialize(phone_code_hash,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = BoolUtil.Deserialize(br);

		}
    }
}
