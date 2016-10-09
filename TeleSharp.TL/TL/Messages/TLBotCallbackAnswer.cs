using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(308605382)]
    public class TLBotCallbackAnswer : TLObject
    {

		
        public override int Constructor
        {
            get
            {
                return 308605382;
            }
        }

             public int flags {get;set;}
     public bool alert {get;set;}
     public string message {get;set;}

		public TLBotCallbackAnswer (bool alert ,string message ){
			this.alert = alert; 
this.message = message; 
	
		}
		public void ComputeFlags()
		{
			flags = 0;
flags = alert ? (flags | 2) : (flags & ~2);
flags = message != null ? (flags | 1) : (flags & ~1);

		}

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
alert = (flags & 2) != 0;
if ((flags & 1) != 0)
message = StringUtil.Deserialize(br);
else
message = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ComputeFlags();
bw.Write(flags);

if ((flags & 1) != 0)
StringUtil.Serialize(message,bw);

        }
    }
}
