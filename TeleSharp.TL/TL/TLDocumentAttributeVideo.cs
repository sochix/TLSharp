using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(1494273227)]
    public class TLDocumentAttributeVideo : TLAbsDocumentAttribute
    {
        public override int Constructor
        {
            get
            {
                return 1494273227;
            }
        }

             public int duration {get;set;}
     public int w {get;set;}
     public int h {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            duration = br.ReadInt32();
w = br.ReadInt32();
h = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(duration);
bw.Write(w);
bw.Write(h);

        }
    }
}
