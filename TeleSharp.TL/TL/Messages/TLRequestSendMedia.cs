using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(-923703407)]
    public class TLRequestSendMedia : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -923703407;
            }
        }

                private int flags {get;set;}
        private bool silent {get;set;}
        private bool background {get;set;}
        private bool clear_draft {get;set;}
        private TLAbsInputPeer peer {get;set;}
        private int? reply_to_msg_id {get;set;}
        private TLAbsInputMedia media {get;set;}
        private long random_id {get;set;}
        private TLAbsReplyMarkup reply_markup {get;set;}
        public TLAbsUpdates Response{ get; set;}

		
		public TLRequestSendMedia (bool silent ,bool background ,bool clear_draft ,TLAbsInputPeer peer ,int? reply_to_msg_id ,TLAbsInputMedia media ,long random_id ,TLAbsReplyMarkup reply_markup ){
			this.silent = silent; 
this.background = background; 
this.clear_draft = clear_draft; 
this.peer = peer; 
this.reply_to_msg_id = reply_to_msg_id; 
this.media = media; 
this.random_id = random_id; 
this.reply_markup = reply_markup; 
	
		}

		public void ComputeFlags()
		{
			flags = 0;
flags = silent ? (flags | 32) : (flags & ~32);
flags = background ? (flags | 64) : (flags & ~64);
flags = clear_draft ? (flags | 128) : (flags & ~128);
flags = reply_to_msg_id != null ? (flags | 1) : (flags & ~1);
flags = reply_markup != null ? (flags | 4) : (flags & ~4);

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

media = (TLAbsInputMedia)ObjectUtils.DeserializeObject(br);
random_id = br.ReadInt64();
if ((flags & 4) != 0)
reply_markup = (TLAbsReplyMarkup)ObjectUtils.DeserializeObject(br);
else
reply_markup = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ComputeFlags();
bw.Write(flags);



ObjectUtils.SerializeObject(peer,bw);
if ((flags & 1) != 0)
bw.Write(reply_to_msg_id.Value);
ObjectUtils.SerializeObject(media,bw);
bw.Write(random_id);
if ((flags & 4) != 0)
ObjectUtils.SerializeObject(reply_markup,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

		}
    }
}
