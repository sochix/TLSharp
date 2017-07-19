using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(400266251)]
    public class TLBotInlineMediaResult : TLAbsBotInlineResult
    {
        public override int Constructor
        {
            get
            {
                return 400266251;
            }
        }

        public int flags { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public TLAbsPhoto photo { get; set; }
        public TLAbsDocument document { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public TLAbsBotInlineMessage send_message { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = photo != null ? (flags | 1) : (flags & ~1);
            flags = document != null ? (flags | 2) : (flags & ~2);
            flags = title != null ? (flags | 4) : (flags & ~4);
            flags = description != null ? (flags | 8) : (flags & ~8);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            id = StringUtil.Deserialize(br);
            type = StringUtil.Deserialize(br);
            if ((flags & 1) != 0)
                photo = (TLAbsPhoto)ObjectUtils.DeserializeObject(br);
            else
                photo = null;

            if ((flags & 2) != 0)
                document = (TLAbsDocument)ObjectUtils.DeserializeObject(br);
            else
                document = null;

            if ((flags & 4) != 0)
                title = StringUtil.Deserialize(br);
            else
                title = null;

            if ((flags & 8) != 0)
                description = StringUtil.Deserialize(br);
            else
                description = null;

            send_message = (TLAbsBotInlineMessage)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            StringUtil.Serialize(id, bw);
            StringUtil.Serialize(type, bw);
            if ((flags & 1) != 0)
                ObjectUtils.SerializeObject(photo, bw);
            if ((flags & 2) != 0)
                ObjectUtils.SerializeObject(document, bw);
            if ((flags & 4) != 0)
                StringUtil.Serialize(title, bw);
            if ((flags & 8) != 0)
                StringUtil.Serialize(description, bw);
            ObjectUtils.SerializeObject(send_message, bw);

        }
    }
}
