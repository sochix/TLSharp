using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
	[TLObject(2018596725)]
    public class TLRequestUpdateProfile : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 2018596725;
            }
        }

                private int flags {get;set;}
        private string first_name {get;set;}
        private string last_name {get;set;}
        private string about {get;set;}
        public TLAbsUser Response{ get; set;}

		
		public TLRequestUpdateProfile (string first_name ,string last_name ,string about ){
			this.first_name = first_name; 
this.last_name = last_name; 
this.about = about; 
	
		}

		public void ComputeFlags()
		{
			flags = 0;
flags = first_name != null ? (flags | 1) : (flags & ~1);
flags = last_name != null ? (flags | 2) : (flags & ~2);
flags = about != null ? (flags | 4) : (flags & ~4);

		}

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
if ((flags & 1) != 0)
first_name = StringUtil.Deserialize(br);
else
first_name = null;

if ((flags & 2) != 0)
last_name = StringUtil.Deserialize(br);
else
last_name = null;

if ((flags & 4) != 0)
about = StringUtil.Deserialize(br);
else
about = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ComputeFlags();
bw.Write(flags);
if ((flags & 1) != 0)
StringUtil.Serialize(first_name,bw);
if ((flags & 2) != 0)
StringUtil.Serialize(last_name,bw);
if ((flags & 4) != 0)
StringUtil.Serialize(about,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (TLAbsUser)ObjectUtils.DeserializeObject(br);

		}
    }
}
