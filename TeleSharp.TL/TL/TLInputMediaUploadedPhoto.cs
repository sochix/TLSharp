using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-139464256)]
    public class TLInputMediaUploadedPhoto : TLAbsInputMedia
    {

		
        public override int Constructor
        {
            get
            {
                return -139464256;
            }
        }

             public TLAbsInputFile file {get;set;}
     public string caption {get;set;}

		public TLInputMediaUploadedPhoto (TLAbsInputFile file ,string caption ){
			this.file = file; 
this.caption = caption; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            file = (TLAbsInputFile)ObjectUtils.DeserializeObject(br);
caption = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(file,bw);
StringUtil.Serialize(caption,bw);

        }
    }
}
