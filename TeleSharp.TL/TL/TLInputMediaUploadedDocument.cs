using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(495530093)]
    public class TLInputMediaUploadedDocument : TLAbsInputMedia
    {

		
        public override int Constructor
        {
            get
            {
                return 495530093;
            }
        }

             public TLAbsInputFile file {get;set;}
     public string mime_type {get;set;}
     public TLVector<TLAbsDocumentAttribute> attributes {get;set;}
     public string caption {get;set;}

		public TLInputMediaUploadedDocument (TLAbsInputFile file ,string mime_type ,TLVector<TLAbsDocumentAttribute> attributes ,string caption ){
			this.file = file; 
this.mime_type = mime_type; 
this.attributes = attributes; 
this.caption = caption; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            file = (TLAbsInputFile)ObjectUtils.DeserializeObject(br);
mime_type = StringUtil.Deserialize(br);
attributes = (TLVector<TLAbsDocumentAttribute>)ObjectUtils.DeserializeVector<TLAbsDocumentAttribute>(br);
caption = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(file,bw);
StringUtil.Serialize(mime_type,bw);
ObjectUtils.SerializeObject(attributes,bw);
StringUtil.Serialize(caption,bw);

        }
    }
}
