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

        public int Flags { get; set; }
        public long Id { get; set; }
        public string Url { get; set; }
        public string DisplayUrl { get; set; }
        public int Hash { get; set; }
        public string Type { get; set; }
        public string SiteName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TLAbsPhoto Photo { get; set; }
        public string EmbedUrl { get; set; }
        public string EmbedType { get; set; }
        public int? EmbedWidth { get; set; }
        public int? EmbedHeight { get; set; }
        public int? Duration { get; set; }
        public string Author { get; set; }
        public TLAbsDocument Document { get; set; }
        public TLAbsPage CachedPage { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Type != null ? (Flags | 1) : (Flags & ~1);
            Flags = SiteName != null ? (Flags | 2) : (Flags & ~2);
            Flags = Title != null ? (Flags | 4) : (Flags & ~4);
            Flags = Description != null ? (Flags | 8) : (Flags & ~8);
            Flags = Photo != null ? (Flags | 16) : (Flags & ~16);
            Flags = EmbedUrl != null ? (Flags | 32) : (Flags & ~32);
            Flags = EmbedType != null ? (Flags | 32) : (Flags & ~32);
            Flags = EmbedWidth != null ? (Flags | 64) : (Flags & ~64);
            Flags = EmbedHeight != null ? (Flags | 64) : (Flags & ~64);
            Flags = Duration != null ? (Flags | 128) : (Flags & ~128);
            Flags = Author != null ? (Flags | 256) : (Flags & ~256);
            Flags = Document != null ? (Flags | 512) : (Flags & ~512);
            Flags = CachedPage != null ? (Flags | 1024) : (Flags & ~1024);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Id = br.ReadInt64();
            Url = StringUtil.Deserialize(br);
            DisplayUrl = StringUtil.Deserialize(br);
            Hash = br.ReadInt32();
            if ((Flags & 1) != 0)
                Type = StringUtil.Deserialize(br);
            else
                Type = null;

            if ((Flags & 2) != 0)
                SiteName = StringUtil.Deserialize(br);
            else
                SiteName = null;

            if ((Flags & 4) != 0)
                Title = StringUtil.Deserialize(br);
            else
                Title = null;

            if ((Flags & 8) != 0)
                Description = StringUtil.Deserialize(br);
            else
                Description = null;

            if ((Flags & 16) != 0)
                Photo = (TLAbsPhoto)ObjectUtils.DeserializeObject(br);
            else
                Photo = null;

            if ((Flags & 32) != 0)
                EmbedUrl = StringUtil.Deserialize(br);
            else
                EmbedUrl = null;

            if ((Flags & 32) != 0)
                EmbedType = StringUtil.Deserialize(br);
            else
                EmbedType = null;

            if ((Flags & 64) != 0)
                EmbedWidth = br.ReadInt32();
            else
                EmbedWidth = null;

            if ((Flags & 64) != 0)
                EmbedHeight = br.ReadInt32();
            else
                EmbedHeight = null;

            if ((Flags & 128) != 0)
                Duration = br.ReadInt32();
            else
                Duration = null;

            if ((Flags & 256) != 0)
                Author = StringUtil.Deserialize(br);
            else
                Author = null;

            if ((Flags & 512) != 0)
                Document = (TLAbsDocument)ObjectUtils.DeserializeObject(br);
            else
                Document = null;

            if ((Flags & 1024) != 0)
                CachedPage = (TLAbsPage)ObjectUtils.DeserializeObject(br);
            else
                CachedPage = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);
            bw.Write(Id);
            StringUtil.Serialize(Url, bw);
            StringUtil.Serialize(DisplayUrl, bw);
            bw.Write(Hash);
            if ((Flags & 1) != 0)
                StringUtil.Serialize(Type, bw);
            if ((Flags & 2) != 0)
                StringUtil.Serialize(SiteName, bw);
            if ((Flags & 4) != 0)
                StringUtil.Serialize(Title, bw);
            if ((Flags & 8) != 0)
                StringUtil.Serialize(Description, bw);
            if ((Flags & 16) != 0)
                ObjectUtils.SerializeObject(Photo, bw);
            if ((Flags & 32) != 0)
                StringUtil.Serialize(EmbedUrl, bw);
            if ((Flags & 32) != 0)
                StringUtil.Serialize(EmbedType, bw);
            if ((Flags & 64) != 0)
                bw.Write(EmbedWidth.Value);
            if ((Flags & 64) != 0)
                bw.Write(EmbedHeight.Value);
            if ((Flags & 128) != 0)
                bw.Write(Duration.Value);
            if ((Flags & 256) != 0)
                StringUtil.Serialize(Author, bw);
            if ((Flags & 512) != 0)
                ObjectUtils.SerializeObject(Document, bw);
            if ((Flags & 1024) != 0)
                ObjectUtils.SerializeObject(CachedPage, bw);

        }
    }
}
