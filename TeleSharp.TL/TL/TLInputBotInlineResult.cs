using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(750510426)]
    public class TLInputBotInlineResult : TLAbsInputBotInlineResult
    {
        public override int Constructor
        {
            get
            {
                return 750510426;
            }
        }

        public int flags { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string thumb_url { get; set; }
        public string content_url { get; set; }
        public string content_type { get; set; }
        public int? w { get; set; }
        public int? h { get; set; }
        public int? duration { get; set; }
        public TLAbsInputBotInlineMessage send_message { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = title != null ? (flags | 2) : (flags & ~2);
            flags = description != null ? (flags | 4) : (flags & ~4);
            flags = url != null ? (flags | 8) : (flags & ~8);
            flags = thumb_url != null ? (flags | 16) : (flags & ~16);
            flags = content_url != null ? (flags | 32) : (flags & ~32);
            flags = content_type != null ? (flags | 32) : (flags & ~32);
            flags = w != null ? (flags | 64) : (flags & ~64);
            flags = h != null ? (flags | 64) : (flags & ~64);
            flags = duration != null ? (flags | 128) : (flags & ~128);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            id = StringUtil.Deserialize(br);
            type = StringUtil.Deserialize(br);
            if ((flags & 2) != 0)
                title = StringUtil.Deserialize(br);
            else
                title = null;

            if ((flags & 4) != 0)
                description = StringUtil.Deserialize(br);
            else
                description = null;

            if ((flags & 8) != 0)
                url = StringUtil.Deserialize(br);
            else
                url = null;

            if ((flags & 16) != 0)
                thumb_url = StringUtil.Deserialize(br);
            else
                thumb_url = null;

            if ((flags & 32) != 0)
                content_url = StringUtil.Deserialize(br);
            else
                content_url = null;

            if ((flags & 32) != 0)
                content_type = StringUtil.Deserialize(br);
            else
                content_type = null;

            if ((flags & 64) != 0)
                w = br.ReadInt32();
            else
                w = null;

            if ((flags & 64) != 0)
                h = br.ReadInt32();
            else
                h = null;

            if ((flags & 128) != 0)
                duration = br.ReadInt32();
            else
                duration = null;

            send_message = (TLAbsInputBotInlineMessage)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            StringUtil.Serialize(id, bw);
            StringUtil.Serialize(type, bw);
            if ((flags & 2) != 0)
                StringUtil.Serialize(title, bw);
            if ((flags & 4) != 0)
                StringUtil.Serialize(description, bw);
            if ((flags & 8) != 0)
                StringUtil.Serialize(url, bw);
            if ((flags & 16) != 0)
                StringUtil.Serialize(thumb_url, bw);
            if ((flags & 32) != 0)
                StringUtil.Serialize(content_url, bw);
            if ((flags & 32) != 0)
                StringUtil.Serialize(content_type, bw);
            if ((flags & 64) != 0)
                bw.Write(w.Value);
            if ((flags & 64) != 0)
                bw.Write(h.Value);
            if ((flags & 128) != 0)
                bw.Write(duration.Value);
            ObjectUtils.SerializeObject(send_message, bw);

        }
    }
}
