using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Photos
{
	[TLObject(-1848823128)]
    public class TLRequestGetUserPhotos : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1848823128;
            }
        }

                private TLAbsInputUser user_id {get;set;}
        private int offset {get;set;}
        private long max_id {get;set;}
        private int limit {get;set;}
        public Photos.TLAbsPhotos Response{ get; set;}

		
		public TLRequestGetUserPhotos (TLAbsInputUser user_id ,int offset ,long max_id ,int limit ){
			this.user_id = user_id; 
this.offset = offset; 
this.max_id = max_id; 
this.limit = limit; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            user_id = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
offset = br.ReadInt32();
max_id = br.ReadInt64();
limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(user_id,bw);
bw.Write(offset);
bw.Write(max_id);
bw.Write(limit);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (Photos.TLAbsPhotos)ObjectUtils.DeserializeObject(br);

		}
    }
}
