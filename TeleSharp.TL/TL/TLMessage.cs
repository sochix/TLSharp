using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1063525281)]
    public class TLMessage : TLAbsMessage
    {
        public override int Constructor
        {
            get
            {
                return -1063525281;
            }
        }

        public int flags { get; set; }
        public bool @out { get; set; }
        public bool mentioned { get; set; }
        public bool media_unread { get; set; }
        public bool silent { get; set; }
        public bool post { get; set; }
        public int id { get; set; }
        public int? from_id { get; set; }
        public TLAbsPeer to_id { get; set; }
        public TLMessageFwdHeader fwd_from { get; set; }
        public int? via_bot_id { get; set; }
        public int? reply_to_msg_id { get; set; }
        public int date { get; set; }
        public string message { get; set; }
        public TLAbsMessageMedia media { get; set; }
        public TLAbsReplyMarkup reply_markup { get; set; }
        public TLVector<TLAbsMessageEntity> entities { get; set; }
        public int? views { get; set; }
        public int? edit_date { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = @out ? (flags | 2) : (flags & ~2);
            flags = mentioned ? (flags | 16) : (flags & ~16);
            flags = media_unread ? (flags | 32) : (flags & ~32);
            flags = silent ? (flags | 8192) : (flags & ~8192);
            flags = post ? (flags | 16384) : (flags & ~16384);
            flags = from_id != null ? (flags | 256) : (flags & ~256);
            flags = fwd_from != null ? (flags | 4) : (flags & ~4);
            flags = via_bot_id != null ? (flags | 2048) : (flags & ~2048);
            flags = reply_to_msg_id != null ? (flags | 8) : (flags & ~8);
            flags = media != null ? (flags | 512) : (flags & ~512);
            flags = reply_markup != null ? (flags | 64) : (flags & ~64);
            flags = entities != null ? (flags | 128) : (flags & ~128);
            flags = views != null ? (flags | 1024) : (flags & ~1024);
            flags = edit_date != null ? (flags | 32768) : (flags & ~32768);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            @out = (flags & 2) != 0;
            mentioned = (flags & 16) != 0;
            media_unread = (flags & 32) != 0;
            silent = (flags & 8192) != 0;
            post = (flags & 16384) != 0;
            id = br.ReadInt32();
            if ((flags & 256) != 0)
                from_id = br.ReadInt32();
            else
                from_id = null;

            to_id = (TLAbsPeer)ObjectUtils.DeserializeObject(br);
            if ((flags & 4) != 0)
                fwd_from = (TLMessageFwdHeader)ObjectUtils.DeserializeObject(br);
            else
                fwd_from = null;

            if ((flags & 2048) != 0)
                via_bot_id = br.ReadInt32();
            else
                via_bot_id = null;

            if ((flags & 8) != 0)
                reply_to_msg_id = br.ReadInt32();
            else
                reply_to_msg_id = null;

            date = br.ReadInt32();
            message = StringUtil.Deserialize(br);
            if ((flags & 512) != 0)
                media = (TLAbsMessageMedia)ObjectUtils.DeserializeObject(br);
            else
                media = null;

            if ((flags & 64) != 0)
                reply_markup = (TLAbsReplyMarkup)ObjectUtils.DeserializeObject(br);
            else
                reply_markup = null;

            if ((flags & 128) != 0)
                entities = (TLVector<TLAbsMessageEntity>)ObjectUtils.DeserializeVector<TLAbsMessageEntity>(br);
            else
                entities = null;

            if ((flags & 1024) != 0)
                views = br.ReadInt32();
            else
                views = null;

            if ((flags & 32768) != 0)
                edit_date = br.ReadInt32();
            else
                edit_date = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);





            bw.Write(id);
            if ((flags & 256) != 0)
                bw.Write(from_id.Value);
            ObjectUtils.SerializeObject(to_id, bw);
            if ((flags & 4) != 0)
                ObjectUtils.SerializeObject(fwd_from, bw);
            if ((flags & 2048) != 0)
                bw.Write(via_bot_id.Value);
            if ((flags & 8) != 0)
                bw.Write(reply_to_msg_id.Value);
            bw.Write(date);
            StringUtil.Serialize(message, bw);
            if ((flags & 512) != 0)
                ObjectUtils.SerializeObject(media, bw);
            if ((flags & 64) != 0)
                ObjectUtils.SerializeObject(reply_markup, bw);
            if ((flags & 128) != 0)
                ObjectUtils.SerializeObject(entities, bw);
            if ((flags & 1024) != 0)
                bw.Write(views.Value);
            if ((flags & 32768) != 0)
                bw.Write(edit_date.Value);

        }
    }
}
