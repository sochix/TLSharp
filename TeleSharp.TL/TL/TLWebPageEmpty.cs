using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-350980120)]
    public class TLWebPageEmpty : TLAbsWebPage
    {

		
        public override int Constructor
        {
            get
            {
                return -350980120;
            }
        }

             public long id {get;set;}

		public TLWebPageEmpty (){}
		public TLWebPageEmpty (long id ){
			this.id = id; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(id);

        }
    }
}
