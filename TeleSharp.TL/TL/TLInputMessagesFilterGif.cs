using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-3644025)]
    public class TLInputMessagesFilterGif : TLAbsMessagesFilter
    {

		
        public override int Constructor
        {
            get
            {
                return -3644025;
            }
        }

        
		
		public TLInputMessagesFilterGif (){
				
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            
        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            
        }
    }
}
