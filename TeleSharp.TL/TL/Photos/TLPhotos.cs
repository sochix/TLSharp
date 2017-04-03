using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Photos
{
	[TLObject(-1916114267)]
    public class TLPhotos : TLAbsPhotos
    {
        public override int Constructor
        {
            get
            {
                return -1916114267;
            }
        }

             public TLVector<TLAbsPhoto> photos {get;set;}
     public TLVector<TLAbsUser> users {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            photos = (TLVector<TLAbsPhoto>)ObjectUtils.DeserializeVector<TLAbsPhoto>(br);
users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(photos,bw);
ObjectUtils.SerializeObject(users,bw);

        }
    }
}
