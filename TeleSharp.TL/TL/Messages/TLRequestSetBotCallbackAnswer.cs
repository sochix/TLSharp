using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(-920136629)]
    public class TLRequestSetBotCallbackAnswer : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -920136629;
            }
        }

                public int flags {get;set;}
        public bool alert {get;set;}
        public long query_id {get;set;}
        public string message {get;set;}
        public string url {get;set;}
        public bool Response{ get; set;}


		public void ComputeFlags()
		{
			flags = 0;
flags = alert ? (flags | 2) : (flags & ~2);
flags = message != null ? (flags | 1) : (flags & ~1);
flags = url != null ? (flags | 4) : (flags & ~4);

		}

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
alert = (flags & 2) != 0;
query_id = br.ReadInt64();
if ((flags & 1) != 0)
message = StringUtil.Deserialize(br);
else
message = null;

if ((flags & 4) != 0)
url = StringUtil.Deserialize(br);
else
url = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ComputeFlags();
bw.Write(flags);

bw.Write(query_id);
if ((flags & 1) != 0)
StringUtil.Serialize(message,bw);
if ((flags & 4) != 0)
StringUtil.Serialize(url,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = BoolUtil.Deserialize(br);

		}
    }
}
