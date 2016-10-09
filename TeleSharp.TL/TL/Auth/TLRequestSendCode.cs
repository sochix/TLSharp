using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
	[TLObject(-2035355412)]
    public class TLRequestSendCode : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -2035355412;
            }
        }

                private int flags {get;set;}
        private bool allow_flashcall {get;set;}
        private string phone_number {get;set;}
        private bool? current_number {get;set;}
        private int api_id {get;set;}
        private string api_hash {get;set;}
        public Auth.TLSentCode Response{ get; set;}

		
		public TLRequestSendCode (bool allow_flashcall ,string phone_number ,bool? current_number ,int api_id ,string api_hash ){
			this.allow_flashcall = allow_flashcall; 
this.phone_number = phone_number; 
this.current_number = current_number; 
this.api_id = api_id; 
this.api_hash = api_hash; 
	
		}

		public void ComputeFlags()
		{
			flags = 0;
flags = allow_flashcall ? (flags | 1) : (flags & ~1);
flags = current_number != null ? (flags | 1) : (flags & ~1);

		}

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
allow_flashcall = (flags & 1) != 0;
phone_number = StringUtil.Deserialize(br);
if ((flags & 1) != 0)
current_number = BoolUtil.Deserialize(br);
else
current_number = null;

api_id = br.ReadInt32();
api_hash = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ComputeFlags();
bw.Write(flags);

StringUtil.Serialize(phone_number,bw);
if ((flags & 1) != 0)
BoolUtil.Serialize(current_number.Value,bw);
bw.Write(api_id);
StringUtil.Serialize(api_hash,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (Auth.TLSentCode)ObjectUtils.DeserializeObject(br);

		}
    }
}
