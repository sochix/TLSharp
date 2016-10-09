using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-1809496270)]
    public class TLInputChatUploadedPhoto : TLAbsInputChatPhoto
    {

		
        public override int Constructor
        {
            get
            {
                return -1809496270;
            }
        }

             public TLAbsInputFile file {get;set;}
     public TLAbsInputPhotoCrop crop {get;set;}

		public TLInputChatUploadedPhoto (TLAbsInputFile file ,TLAbsInputPhotoCrop crop ){
			this.file = file; 
this.crop = crop; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            file = (TLAbsInputFile)ObjectUtils.DeserializeObject(br);
crop = (TLAbsInputPhotoCrop)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(file,bw);
ObjectUtils.SerializeObject(crop,bw);

        }
    }
}
