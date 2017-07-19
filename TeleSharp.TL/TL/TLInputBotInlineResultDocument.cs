using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-459324)]
    public class TLInputBotInlineResultDocument : TLAbsInputBotInlineResult
    {
        public override int Constructor
        {
            get
            {
                return -459324;
            }
        }

        public int flags { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public TLAbsInputDocument document { get; set; }
        public TLAbsInputBotInlineMessage send_message { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = title != null ? (flags | 2) : (flags & ~2);
            flags = description != null ? (flags | 4) : (flags & ~4);

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

            document = (TLAbsInputDocument)ObjectUtils.DeserializeObject(br);
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
            ObjectUtils.SerializeObject(document, bw);
            ObjectUtils.SerializeObject(send_message, bw);

        }
    }
}
