using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Upload
{
	[TLObject(-1291540959)]
    public class TLRequestSaveFilePart : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1291540959;
            }
        }

                private long file_id {get;set;}
        private int file_part {get;set;}
        private byte[] bytes {get;set;}
        public bool Response{ get; set;}

		
		public TLRequestSaveFilePart (long file_id ,int file_part ,byte[] bytes ){
			this.file_id = file_id; 
this.file_part = file_part; 
this.bytes = bytes; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            file_id = br.ReadInt64();
file_part = br.ReadInt32();
bytes = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(file_id);
bw.Write(file_part);
BytesUtil.Serialize(bytes,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = BoolUtil.Deserialize(br);

		}
    }
}
