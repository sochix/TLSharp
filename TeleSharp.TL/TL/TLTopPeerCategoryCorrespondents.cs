using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(104314861)]
    public class TLTopPeerCategoryCorrespondents : TLAbsTopPeerCategory
    {

		
        public override int Constructor
        {
            get
            {
                return 104314861;
            }
        }

        
		
		public TLTopPeerCategoryCorrespondents (){
				
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
