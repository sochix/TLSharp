using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(1938967520)]
    public class TLMessageEntityPre : TLAbsMessageEntity
    {

		
        public override int Constructor
        {
            get
            {
                return 1938967520;
            }
        }

             public int offset {get;set;}
     public int length {get;set;}
     public string language {get;set;}

		public TLMessageEntityPre (int offset ,int length ,string language ){
			this.offset = offset; 
this.length = length; 
this.language = language; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            offset = br.ReadInt32();
length = br.ReadInt32();
language = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(offset);
bw.Write(length);
StringUtil.Serialize(language,bw);

        }
    }
}
