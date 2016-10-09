using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Upload
{
	[TLObject(-562337987)]
    public class TLRequestSaveBigFilePart : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -562337987;
            }
        }

                private long file_id {get;set;}
        private int file_part {get;set;}
        private int file_total_parts {get;set;}
        private byte[] bytes {get;set;}
        public bool Response{ get; set;}

		
		public TLRequestSaveBigFilePart (long file_id ,int file_part ,int file_total_parts ,byte[] bytes ){
			this.file_id = file_id; 
this.file_part = file_part; 
this.file_total_parts = file_total_parts; 
this.bytes = bytes; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            file_id = br.ReadInt64();
file_part = br.ReadInt32();
file_total_parts = br.ReadInt32();
bytes = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(file_id);
bw.Write(file_part);
bw.Write(file_total_parts);
BytesUtil.Serialize(bytes,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = BoolUtil.Deserialize(br);

		}
    }
}
