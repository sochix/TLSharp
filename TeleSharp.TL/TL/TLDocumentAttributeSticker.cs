using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(978674434)]
    public class TLDocumentAttributeSticker : TLAbsDocumentAttribute
    {

		
        public override int Constructor
        {
            get
            {
                return 978674434;
            }
        }

             public string alt {get;set;}
     public TLAbsInputStickerSet stickerset {get;set;}

		public TLDocumentAttributeSticker (string alt ,TLAbsInputStickerSet stickerset ){
			this.alt = alt; 
this.stickerset = stickerset; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            alt = StringUtil.Deserialize(br);
stickerset = (TLAbsInputStickerSet)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            StringUtil.Serialize(alt,bw);
ObjectUtils.SerializeObject(stickerset,bw);

        }
    }
}
