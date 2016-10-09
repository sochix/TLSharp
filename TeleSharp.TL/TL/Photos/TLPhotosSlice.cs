using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Photos
{
	[TLObject(352657236)]
    public class TLPhotosSlice : TLAbsPhotos
    {

		
        public override int Constructor
        {
            get
            {
                return 352657236;
            }
        }

             public int count {get;set;}
     public TLVector<TLAbsPhoto> photos {get;set;}
     public TLVector<TLAbsUser> users {get;set;}

		public TLPhotosSlice (int count ,TLVector<TLAbsPhoto> photos ,TLVector<TLAbsUser> users ){
			this.count = count; 
this.photos = photos; 
this.users = users; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            count = br.ReadInt32();
photos = (TLVector<TLAbsPhoto>)ObjectUtils.DeserializeVector<TLAbsPhoto>(br);
users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(count);
ObjectUtils.SerializeObject(photos,bw);
ObjectUtils.SerializeObject(users,bw);

        }
    }
}
