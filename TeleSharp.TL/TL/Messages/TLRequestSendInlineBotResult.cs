using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(-1318189314)]
    public class TLRequestSendInlineBotResult : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1318189314;
            }
        }

                private int flags {get;set;}
        private bool silent {get;set;}
        private bool background {get;set;}
        private bool clear_draft {get;set;}
        private TLAbsInputPeer peer {get;set;}
        private int? reply_to_msg_id {get;set;}
        private long random_id {get;set;}
        private long query_id {get;set;}
        private string id {get;set;}
        public TLAbsUpdates Response{ get; set;}

		
		public TLRequestSendInlineBotResult (bool silent ,bool background ,bool clear_draft ,TLAbsInputPeer peer ,int? reply_to_msg_id ,long random_id ,long query_id ,string id ){
			this.silent = silent; 
this.background = background; 
this.clear_draft = clear_draft; 
this.peer = peer; 
this.reply_to_msg_id = reply_to_msg_id; 
this.random_id = random_id; 
this.query_id = query_id; 
this.id = id; 
	
		}

		public void ComputeFlags()
		{
			flags = 0;
flags = silent ? (flags | 32) : (flags & ~32);
flags = background ? (flags | 64) : (flags & ~64);
flags = clear_draft ? (flags | 128) : (flags & ~128);
flags = reply_to_msg_id != null ? (flags | 1) : (flags & ~1);

		}

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
silent = (flags & 32) != 0;
background = (flags & 64) != 0;
clear_draft = (flags & 128) != 0;
peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
if ((flags & 1) != 0)
reply_to_msg_id = br.ReadInt32();
else
reply_to_msg_id = null;

random_id = br.ReadInt64();
query_id = br.ReadInt64();
id = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ComputeFlags();
bw.Write(flags);



ObjectUtils.SerializeObject(peer,bw);
if ((flags & 1) != 0)
bw.Write(reply_to_msg_id.Value);
bw.Write(random_id);
bw.Write(query_id);
StringUtil.Serialize(id,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

		}
    }
}
