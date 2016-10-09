using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Upload
{
	[TLObject(-475607115)]
    public class TLRequestGetFile : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -475607115;
            }
        }

                private TLAbsInputFileLocation location {get;set;}
        private int offset {get;set;}
        private int limit {get;set;}
        public Upload.TLFile Response{ get; set;}

		
		public TLRequestGetFile (TLAbsInputFileLocation location ,int offset ,int limit ){
			this.location = location; 
this.offset = offset; 
this.limit = limit; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            location = (TLAbsInputFileLocation)ObjectUtils.DeserializeObject(br);
offset = br.ReadInt32();
limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(location,bw);
bw.Write(offset);
bw.Write(limit);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (Upload.TLFile)ObjectUtils.DeserializeObject(br);

		}
    }
}
