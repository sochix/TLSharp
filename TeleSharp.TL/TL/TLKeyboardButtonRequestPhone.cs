using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-1318425559)]
    public class TLKeyboardButtonRequestPhone : TLAbsKeyboardButton
    {

		
        public override int Constructor
        {
            get
            {
                return -1318425559;
            }
        }

             public string text {get;set;}

		public TLKeyboardButtonRequestPhone (){
			
		}
		public TLKeyboardButtonRequestPhone (string text ){
			this.text = text; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            text = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            StringUtil.Serialize(text,bw);

        }
    }
}
