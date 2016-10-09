using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Photos
{
	[TLObject(-285902432)]
    public class TLRequestUpdateProfilePhoto : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -285902432;
            }
        }

                private TLAbsInputPhoto id {get;set;}
        private TLAbsInputPhotoCrop crop {get;set;}
        public TLAbsUserProfilePhoto Response{ get; set;}

		
		public TLRequestUpdateProfilePhoto (TLAbsInputPhoto id ,TLAbsInputPhotoCrop crop ){
			this.id = id; 
this.crop = crop; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            id = (TLAbsInputPhoto)ObjectUtils.DeserializeObject(br);
crop = (TLAbsInputPhotoCrop)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(id,bw);
ObjectUtils.SerializeObject(crop,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (TLAbsUserProfilePhoto)ObjectUtils.DeserializeObject(br);

		}
    }
}
