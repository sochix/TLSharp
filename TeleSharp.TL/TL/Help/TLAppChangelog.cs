using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Help
{
	[TLObject(1181279933)]
    public class TLAppChangelog : TLAbsAppChangelog
    {

		
        public override int Constructor
        {
            get
            {
                return 1181279933;
            }
        }

             public string text {get;set;}

		public TLAppChangelog (){}
		public TLAppChangelog (string text ){
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
