using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-840088834)]
    public class TLPhoto : TLAbsPhoto
    {

		
        public override int Constructor
        {
            get
            {
                return -840088834;
            }
        }

             public long id {get;set;}
     public long access_hash {get;set;}
     public int date {get;set;}
     public TLVector<TLAbsPhotoSize> sizes {get;set;}

		public TLPhoto (long id ,long access_hash ,int date ,TLVector<TLAbsPhotoSize> sizes ){
			this.id = id; 
this.access_hash = access_hash; 
this.date = date; 
this.sizes = sizes; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt64();
access_hash = br.ReadInt64();
date = br.ReadInt32();
sizes = (TLVector<TLAbsPhotoSize>)ObjectUtils.DeserializeVector<TLAbsPhotoSize>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(id);
bw.Write(access_hash);
bw.Write(date);
ObjectUtils.SerializeObject(sizes,bw);

        }
    }
}
