using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-1494984313)]
    public class TLInputMediaContact : TLAbsInputMedia
    {

		
        public override int Constructor
        {
            get
            {
                return -1494984313;
            }
        }

             public string phone_number {get;set;}
     public string first_name {get;set;}
     public string last_name {get;set;}

		public TLInputMediaContact (){}
		public TLInputMediaContact (string phone_number ,string first_name ,string last_name ){
			this.phone_number = phone_number; 
this.first_name = first_name; 
this.last_name = last_name; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            phone_number = StringUtil.Deserialize(br);
first_name = StringUtil.Deserialize(br);
last_name = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            StringUtil.Serialize(phone_number,bw);
StringUtil.Serialize(first_name,bw);
StringUtil.Serialize(last_name,bw);

        }
    }
}
