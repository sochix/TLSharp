using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(483901197)]
    public class TLInputPhotoEmpty : TLAbsInputPhoto
    {

		
        public override int Constructor
        {
            get
            {
                return 483901197;
            }
        }

        
		public TLInputPhotoEmpty (){
			
		}
		public TLInputPhotoEmpty (){
				
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
