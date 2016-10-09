using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(-1137057461)]
    public class TLRequestSaveDraft : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1137057461;
            }
        }

                private int flags {get;set;}
        private bool no_webpage {get;set;}
        private int? reply_to_msg_id {get;set;}
        private TLAbsInputPeer peer {get;set;}
        private string message {get;set;}
        private TLVector<TLAbsMessageEntity> entities {get;set;}
        public bool Response{ get; set;}

		
		public TLRequestSaveDraft (bool no_webpage ,int? reply_to_msg_id ,TLAbsInputPeer peer ,string message ,TLVector<TLAbsMessageEntity> entities ){
			this.no_webpage = no_webpage; 
this.reply_to_msg_id = reply_to_msg_id; 
this.peer = peer; 
this.message = message; 
this.entities = entities; 
	
		}

		public void ComputeFlags()
		{
			flags = 0;
flags = no_webpage ? (flags | 2) : (flags & ~2);
flags = reply_to_msg_id != null ? (flags | 1) : (flags & ~1);
flags = entities != null ? (flags | 8) : (flags & ~8);

		}

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
no_webpage = (flags & 2) != 0;
if ((flags & 1) != 0)
reply_to_msg_id = br.ReadInt32();
else
reply_to_msg_id = null;

peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
message = StringUtil.Deserialize(br);
if ((flags & 8) != 0)
entities = (TLVector<TLAbsMessageEntity>)ObjectUtils.DeserializeVector<TLAbsMessageEntity>(br);
else
entities = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ComputeFlags();
bw.Write(flags);

if ((flags & 1) != 0)
bw.Write(reply_to_msg_id.Value);
ObjectUtils.SerializeObject(peer,bw);
StringUtil.Serialize(message,bw);
if ((flags & 8) != 0)
ObjectUtils.SerializeObject(entities,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = BoolUtil.Deserialize(br);

		}
    }
}
