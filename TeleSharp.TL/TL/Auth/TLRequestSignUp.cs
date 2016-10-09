using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
	[TLObject(453408308)]
    public class TLRequestSignUp : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 453408308;
            }
        }

                private string phone_number {get;set;}
        private string phone_code_hash {get;set;}
        private string phone_code {get;set;}
        private string first_name {get;set;}
        private string last_name {get;set;}
        public Auth.TLAuthorization Response{ get; set;}

		
		public TLRequestSignUp (string phone_number ,string phone_code_hash ,string phone_code ,string first_name ,string last_name ){
			this.phone_number = phone_number; 
this.phone_code_hash = phone_code_hash; 
this.phone_code = phone_code; 
this.first_name = first_name; 
this.last_name = last_name; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            phone_number = StringUtil.Deserialize(br);
phone_code_hash = StringUtil.Deserialize(br);
phone_code = StringUtil.Deserialize(br);
first_name = StringUtil.Deserialize(br);
last_name = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            StringUtil.Serialize(phone_number,bw);
StringUtil.Serialize(phone_code_hash,bw);
StringUtil.Serialize(phone_code,bw);
StringUtil.Serialize(first_name,bw);
StringUtil.Serialize(last_name,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (Auth.TLAuthorization)ObjectUtils.DeserializeObject(br);

		}
    }
}
