using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-367298028)]
    public class TLKeyboardButtonSwitchInline : TLAbsKeyboardButton
    {

		
        public override int Constructor
        {
            get
            {
                return -367298028;
            }
        }

             public string text {get;set;}
     public string query {get;set;}

		public TLKeyboardButtonSwitchInline (){}
		public TLKeyboardButtonSwitchInline (string text ,string query ){
			this.text = text; 
this.query = query; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            text = StringUtil.Deserialize(br);
query = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            StringUtil.Serialize(text,bw);
StringUtil.Serialize(query,bw);

        }
    }
}
