using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-1343524562)]
    public class TLInputChannel : TLAbsInputChannel
    {

		
        public override int Constructor
        {
            get
            {
                return -1343524562;
            }
        }

             public int channel_id {get;set;}
     public long access_hash {get;set;}

		public TLInputChannel (){}
		public TLInputChannel (int channel_id ,long access_hash ){
			this.channel_id = channel_id; 
this.access_hash = access_hash; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            channel_id = br.ReadInt32();
access_hash = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(channel_id);
bw.Write(access_hash);

        }
    }
}
