using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-1386138479)]
    public class TLInputMediaUploadedThumbDocument : TLAbsInputMedia
    {

		
        public override int Constructor
        {
            get
            {
                return -1386138479;
            }
        }

             public TLAbsInputFile file {get;set;}
     public TLAbsInputFile thumb {get;set;}
     public string mime_type {get;set;}
     public TLVector<TLAbsDocumentAttribute> attributes {get;set;}
     public string caption {get;set;}

		public TLInputMediaUploadedThumbDocument (TLAbsInputFile file ,TLAbsInputFile thumb ,string mime_type ,TLVector<TLAbsDocumentAttribute> attributes ,string caption ){
			this.file = file; 
this.thumb = thumb; 
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
thumb = (TLAbsInputFile)ObjectUtils.DeserializeObject(br);
mime_type = StringUtil.Deserialize(br);
attributes = (TLVector<TLAbsDocumentAttribute>)ObjectUtils.DeserializeVector<TLAbsDocumentAttribute>(br);
caption = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(file,bw);
ObjectUtils.SerializeObject(thumb,bw);
StringUtil.Serialize(mime_type,bw);
ObjectUtils.SerializeObject(attributes,bw);
StringUtil.Serialize(caption,bw);

        }
    }
}
