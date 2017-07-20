using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1594340540)]
    public class TLWebPage : TLAbsWebPage
    {
        public override int Constructor
        {
            get
            {
                return 1594340540;
            }
        }

        public int flags { get; set; }
        public long id { get; set; }
        public string url { get; set; }
        public string display_url { get; set; }
        public int hash { get; set; }
        public string type { get; set; }
        public string site_name { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public TLAbsPhoto photo { get; set; }
        public string embed_url { get; set; }
        public string embed_type { get; set; }
        public int? embed_width { get; set; }
        public int? embed_height { get; set; }
        public int? duration { get; set; }
        public string author { get; set; }
        public TLAbsDocument document { get; set; }
        public TLAbsPage cached_page { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = type != null ? (flags | 1) : (flags & ~1);
            flags = site_name != null ? (flags | 2) : (flags & ~2);
            flags = title != null ? (flags | 4) : (flags & ~4);
            flags = description != null ? (flags | 8) : (flags & ~8);
            flags = photo != null ? (flags | 16) : (flags & ~16);
            flags = embed_url != null ? (flags | 32) : (flags & ~32);
            flags = embed_type != null ? (flags | 32) : (flags & ~32);
            flags = embed_width != null ? (flags | 64) : (flags & ~64);
            flags = embed_height != null ? (flags | 64) : (flags & ~64);
            flags = duration != null ? (flags | 128) : (flags & ~128);
            flags = author != null ? (flags | 256) : (flags & ~256);
            flags = document != null ? (flags | 512) : (flags & ~512);
            flags = cached_page != null ? (flags | 1024) : (flags & ~1024);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            id = br.ReadInt64();
            url = StringUtil.Deserialize(br);
            display_url = StringUtil.Deserialize(br);
            hash = br.ReadInt32();
            if ((flags & 1) != 0)
                type = StringUtil.Deserialize(br);
            else
                type = null;

            if ((flags & 2) != 0)
                site_name = StringUtil.Deserialize(br);
            else
                site_name = null;

            if ((flags & 4) != 0)
                title = StringUtil.Deserialize(br);
            else
                title = null;

            if ((flags & 8) != 0)
                description = StringUtil.Deserialize(br);
            else
                description = null;

            if ((flags & 16) != 0)
                photo = (TLAbsPhoto)ObjectUtils.DeserializeObject(br);
            else
                photo = null;

            if ((flags & 32) != 0)
                embed_url = StringUtil.Deserialize(br);
            else
                embed_url = null;

            if ((flags & 32) != 0)
                embed_type = StringUtil.Deserialize(br);
            else
                embed_type = null;

            if ((flags & 64) != 0)
                embed_width = br.ReadInt32();
            else
                embed_width = null;

            if ((flags & 64) != 0)
                embed_height = br.ReadInt32();
            else
                embed_height = null;

            if ((flags & 128) != 0)
                duration = br.ReadInt32();
            else
                duration = null;

            if ((flags & 256) != 0)
                author = StringUtil.Deserialize(br);
            else
                author = null;

            if ((flags & 512) != 0)
                document = (TLAbsDocument)ObjectUtils.DeserializeObject(br);
            else
                document = null;

            if ((flags & 1024) != 0)
                cached_page = (TLAbsPage)ObjectUtils.DeserializeObject(br);
            else
                cached_page = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            bw.Write(id);
            StringUtil.Serialize(url, bw);
            StringUtil.Serialize(display_url, bw);
            bw.Write(hash);
            if ((flags & 1) != 0)
                StringUtil.Serialize(type, bw);
            if ((flags & 2) != 0)
                StringUtil.Serialize(site_name, bw);
            if ((flags & 4) != 0)
                StringUtil.Serialize(title, bw);
            if ((flags & 8) != 0)
                StringUtil.Serialize(description, bw);
            if ((flags & 16) != 0)
                ObjectUtils.SerializeObject(photo, bw);
            if ((flags & 32) != 0)
                StringUtil.Serialize(embed_url, bw);
            if ((flags & 32) != 0)
                StringUtil.Serialize(embed_type, bw);
            if ((flags & 64) != 0)
                bw.Write(embed_width.Value);
            if ((flags & 64) != 0)
                bw.Write(embed_height.Value);
            if ((flags & 128) != 0)
                bw.Write(duration.Value);
            if ((flags & 256) != 0)
                StringUtil.Serialize(author, bw);
            if ((flags & 512) != 0)
                ObjectUtils.SerializeObject(document, bw);
            if ((flags & 1024) != 0)
                ObjectUtils.SerializeObject(cached_page, bw);

        }
    }
}
