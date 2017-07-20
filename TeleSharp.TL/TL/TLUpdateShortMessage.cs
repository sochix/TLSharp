using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1857044719)]
    public class TLUpdateShortMessage : TLAbsUpdates
    {
        public override int Constructor
        {
            get
            {
                return -1857044719;
            }
        }

        public int flags { get; set; }
        public bool @out { get; set; }
        public bool mentioned { get; set; }
        public bool media_unread { get; set; }
        public bool silent { get; set; }
        public int id { get; set; }
        public int user_id { get; set; }
        public string message { get; set; }
        public int pts { get; set; }
        public int pts_count { get; set; }
        public int date { get; set; }
        public TLMessageFwdHeader fwd_from { get; set; }
        public int? via_bot_id { get; set; }
        public int? reply_to_msg_id { get; set; }
        public TLVector<TLAbsMessageEntity> entities { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = @out ? (flags | 2) : (flags & ~2);
            flags = mentioned ? (flags | 16) : (flags & ~16);
            flags = media_unread ? (flags | 32) : (flags & ~32);
            flags = silent ? (flags | 8192) : (flags & ~8192);
            flags = fwd_from != null ? (flags | 4) : (flags & ~4);
            flags = via_bot_id != null ? (flags | 2048) : (flags & ~2048);
            flags = reply_to_msg_id != null ? (flags | 8) : (flags & ~8);
            flags = entities != null ? (flags | 128) : (flags & ~128);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            @out = (flags & 2) != 0;
            mentioned = (flags & 16) != 0;
            media_unread = (flags & 32) != 0;
            silent = (flags & 8192) != 0;
            id = br.ReadInt32();
            user_id = br.ReadInt32();
            message = StringUtil.Deserialize(br);
            pts = br.ReadInt32();
            pts_count = br.ReadInt32();
            date = br.ReadInt32();
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

            if ((flags & 128) != 0)
                entities = (TLVector<TLAbsMessageEntity>)ObjectUtils.DeserializeVector<TLAbsMessageEntity>(br);
            else
                entities = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);




            bw.Write(id);
            bw.Write(user_id);
            StringUtil.Serialize(message, bw);
            bw.Write(pts);
            bw.Write(pts_count);
            bw.Write(date);
            if ((flags & 4) != 0)
                ObjectUtils.SerializeObject(fwd_from, bw);
            if ((flags & 2048) != 0)
                bw.Write(via_bot_id.Value);
            if ((flags & 8) != 0)
                bw.Write(reply_to_msg_id.Value);
            if ((flags & 128) != 0)
                ObjectUtils.SerializeObject(entities, bw);

        }
    }
}
