using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Photos
{
	[TLObject(-720397176)]
    public class TLRequestUploadProfilePhoto : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -720397176;
            }
        }

                private TLAbsInputFile file {get;set;}
        private string caption {get;set;}
        private TLAbsInputGeoPoint geo_point {get;set;}
        private TLAbsInputPhotoCrop crop {get;set;}
        public Photos.TLPhoto Response{ get; set;}

		
		public TLRequestUploadProfilePhoto (TLAbsInputFile file ,string caption ,TLAbsInputGeoPoint geo_point ,TLAbsInputPhotoCrop crop ){
			this.file = file; 
this.caption = caption; 
this.geo_point = geo_point; 
this.crop = crop; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            file = (TLAbsInputFile)ObjectUtils.DeserializeObject(br);
caption = StringUtil.Deserialize(br);
geo_point = (TLAbsInputGeoPoint)ObjectUtils.DeserializeObject(br);
crop = (TLAbsInputPhotoCrop)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(file,bw);
StringUtil.Serialize(caption,bw);
ObjectUtils.SerializeObject(geo_point,bw);
ObjectUtils.SerializeObject(crop,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (Photos.TLPhoto)ObjectUtils.DeserializeObject(br);

		}
    }
}
