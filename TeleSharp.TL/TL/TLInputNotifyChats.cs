using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(1251338318)]
    public class TLInputNotifyChats : TLAbsInputNotifyPeer
    {

		
        public override int Constructor
        {
            get
            {
                return 1251338318;
            }
        }

        
		public TLInputNotifyChats (){
			
		}
		public TLInputNotifyChats (){
				
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            
        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            
        }
    }
}
