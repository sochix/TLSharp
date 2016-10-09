using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(537022650)]
    public class TLUserEmpty : TLAbsUser
    {

		
        public override int Constructor
        {
            get
            {
                return 537022650;
            }
        }

             public int id {get;set;}

		public TLUserEmpty (){}
		public TLUserEmpty (int id ){
			this.id = id; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(id);

        }
    }
}
