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

        public int Flags { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public TLAbsPhoto Photo { get; set; }
        public TLAbsDocument Document { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TLAbsBotInlineMessage SendMessage { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Photo != null ? (Flags | 1) : (Flags & ~1);
            Flags = Document != null ? (Flags | 2) : (Flags & ~2);
            Flags = Title != null ? (Flags | 4) : (Flags & ~4);
            Flags = Description != null ? (Flags | 8) : (Flags & ~8);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Id = StringUtil.Deserialize(br);
            Type = StringUtil.Deserialize(br);
            if ((Flags & 1) != 0)
                Photo = (TLAbsPhoto)ObjectUtils.DeserializeObject(br);
            else
                Photo = null;

            if ((Flags & 2) != 0)
                Document = (TLAbsDocument)ObjectUtils.DeserializeObject(br);
            else
                Document = null;

            if ((Flags & 4) != 0)
                Title = StringUtil.Deserialize(br);
            else
                Title = null;

            if ((Flags & 8) != 0)
                Description = StringUtil.Deserialize(br);
            else
                Description = null;

            SendMessage = (TLAbsBotInlineMessage)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);
            StringUtil.Serialize(Id, bw);
            StringUtil.Serialize(Type, bw);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(Photo, bw);
            if ((Flags & 2) != 0)
                ObjectUtils.SerializeObject(Document, bw);
            if ((Flags & 4) != 0)
                StringUtil.Serialize(Title, bw);
            if ((Flags & 8) != 0)
                StringUtil.Serialize(Description, bw);
            ObjectUtils.SerializeObject(SendMessage, bw);

        }
    }
}
