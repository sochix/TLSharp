using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(406307684)]
    public class TLInputEncryptedFileEmpty : TLAbsInputEncryptedFile
    {

		
        public override int Constructor
        {
            get
            {
                return 406307684;
            }
        }

        
		
		public TLInputEncryptedFileEmpty (){
				
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
