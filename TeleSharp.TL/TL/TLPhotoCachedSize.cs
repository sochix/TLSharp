using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-374917894)]
    public class TLPhotoCachedSize : TLAbsPhotoSize
    {

		
        public override int Constructor
        {
            get
            {
                return -374917894;
            }
        }

             public string type {get;set;}
     public TLAbsFileLocation location {get;set;}
     public int w {get;set;}
     public int h {get;set;}
     public byte[] bytes {get;set;}

		public TLPhotoCachedSize (string type ,TLAbsFileLocation location ,int w ,int h ,byte[] bytes ){
			this.type = type; 
this.location = location; 
this.w = w; 
this.h = h; 
this.bytes = bytes; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            type = StringUtil.Deserialize(br);
location = (TLAbsFileLocation)ObjectUtils.DeserializeObject(br);
w = br.ReadInt32();
h = br.ReadInt32();
bytes = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            StringUtil.Serialize(type,bw);
ObjectUtils.SerializeObject(location,bw);
bw.Write(w);
bw.Write(h);
BytesUtil.Serialize(bytes,bw);

        }
    }
}
