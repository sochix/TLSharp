using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-1781355374)]
    public class TLMessageActionChannelCreate : TLAbsMessageAction
    {

		
        public override int Constructor
        {
            get
            {
                return -1781355374;
            }
        }

             public string title {get;set;}

		public TLMessageActionChannelCreate (){}
		public TLMessageActionChannelCreate (string title ){
			this.title = title; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            title = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            StringUtil.Serialize(title,bw);

        }
    }
}
