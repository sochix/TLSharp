using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-1670052855)]
    public class TLFoundGifCached : TLAbsFoundGif
    {

		
        public override int Constructor
        {
            get
            {
                return -1670052855;
            }
        }

             public string url {get;set;}
     public TLAbsPhoto photo {get;set;}
     public TLAbsDocument document {get;set;}

		public TLFoundGifCached (){
			
		}
		public TLFoundGifCached (string url ,TLAbsPhoto photo ,TLAbsDocument document ){
			this.url = url; 
this.photo = photo; 
this.document = document; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            url = StringUtil.Deserialize(br);
photo = (TLAbsPhoto)ObjectUtils.DeserializeObject(br);
document = (TLAbsDocument)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            StringUtil.Serialize(url,bw);
ObjectUtils.SerializeObject(photo,bw);
ObjectUtils.SerializeObject(document,bw);

        }
    }
}
