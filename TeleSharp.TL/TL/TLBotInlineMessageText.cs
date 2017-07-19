using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1937807902)]
    public class TLBotInlineMessageText : TLAbsBotInlineMessage
    {
        public override int Constructor
        {
            get
            {
                return -1937807902;
            }
        }

        public int flags { get; set; }
        public bool no_webpage { get; set; }
        public string message { get; set; }
        public TLVector<TLAbsMessageEntity> entities { get; set; }
        public TLAbsReplyMarkup reply_markup { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = no_webpage ? (flags | 1) : (flags & ~1);
            flags = entities != null ? (flags | 2) : (flags & ~2);
            flags = reply_markup != null ? (flags | 4) : (flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            no_webpage = (flags & 1) != 0;
            message = StringUtil.Deserialize(br);
            if ((flags & 2) != 0)
                entities = (TLVector<TLAbsMessageEntity>)ObjectUtils.DeserializeVector<TLAbsMessageEntity>(br);
            else
                entities = null;

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

            StringUtil.Serialize(message, bw);
            if ((flags & 2) != 0)
                ObjectUtils.SerializeObject(entities, bw);
            if ((flags & 4) != 0)
                ObjectUtils.SerializeObject(reply_markup, bw);

        }
    }
}
