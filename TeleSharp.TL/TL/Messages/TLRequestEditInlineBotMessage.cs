using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(319564933)]
    public class TLRequestEditInlineBotMessage : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 319564933;
            }
        }

                private int flags {get;set;}
        private bool no_webpage {get;set;}
        private TLInputBotInlineMessageID id {get;set;}
        private string message {get;set;}
        private TLAbsReplyMarkup reply_markup {get;set;}
        private TLVector<TLAbsMessageEntity> entities {get;set;}
        public bool Response{ get; set;}

		
		public TLRequestEditInlineBotMessage (bool no_webpage ,TLInputBotInlineMessageID id ,string message ,TLAbsReplyMarkup reply_markup ,TLVector<TLAbsMessageEntity> entities ){
			this.no_webpage = no_webpage; 
this.id = id; 
this.message = message; 
this.reply_markup = reply_markup; 
this.entities = entities; 
	
		}

		public void ComputeFlags()
		{
			flags = 0;
flags = no_webpage ? (flags | 2) : (flags & ~2);
flags = message != null ? (flags | 2048) : (flags & ~2048);
flags = reply_markup != null ? (flags | 4) : (flags & ~4);
flags = entities != null ? (flags | 8) : (flags & ~8);

		}

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
no_webpage = (flags & 2) != 0;
id = (TLInputBotInlineMessageID)ObjectUtils.DeserializeObject(br);
if ((flags & 2048) != 0)
message = StringUtil.Deserialize(br);
else
message = null;

if ((flags & 4) != 0)
reply_markup = (TLAbsReplyMarkup)ObjectUtils.DeserializeObject(br);
else
reply_markup = null;

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

ObjectUtils.SerializeObject(id,bw);
if ((flags & 2048) != 0)
StringUtil.Serialize(message,bw);
if ((flags & 4) != 0)
ObjectUtils.SerializeObject(reply_markup,bw);
if ((flags & 8) != 0)
ObjectUtils.SerializeObject(entities,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = BoolUtil.Deserialize(br);

		}
    }
}
