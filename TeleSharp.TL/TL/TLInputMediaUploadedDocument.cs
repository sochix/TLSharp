using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-476700163)]
    public class TLInputMediaUploadedDocument : TLAbsInputMedia
    {
        public override int Constructor
        {
            get
            {
                return -476700163;
            }
        }

        public int flags { get; set; }
        public TLAbsInputFile file { get; set; }
        public TLAbsInputFile thumb { get; set; }
        public string mime_type { get; set; }
        public TLVector<TLAbsDocumentAttribute> attributes { get; set; }
        public string caption { get; set; }
        public TLVector<TLAbsInputDocument> stickers { get; set; }
        public int? ttl_seconds { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = thumb != null ? (flags | 4) : (flags & ~4);
            flags = stickers != null ? (flags | 1) : (flags & ~1);
            flags = ttl_seconds != null ? (flags | 2) : (flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            file = (TLAbsInputFile)ObjectUtils.DeserializeObject(br);
            if ((flags & 4) != 0)
                thumb = (TLAbsInputFile)ObjectUtils.DeserializeObject(br);
            else
                thumb = null;

            mime_type = StringUtil.Deserialize(br);
            attributes = (TLVector<TLAbsDocumentAttribute>)ObjectUtils.DeserializeVector<TLAbsDocumentAttribute>(br);
            caption = StringUtil.Deserialize(br);
            if ((flags & 1) != 0)
                stickers = (TLVector<TLAbsInputDocument>)ObjectUtils.DeserializeVector<TLAbsInputDocument>(br);
            else
                stickers = null;

            if ((flags & 2) != 0)
                ttl_seconds = br.ReadInt32();
            else
                ttl_seconds = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            ObjectUtils.SerializeObject(file, bw);
            if ((flags & 4) != 0)
                ObjectUtils.SerializeObject(thumb, bw);
            StringUtil.Serialize(mime_type, bw);
            ObjectUtils.SerializeObject(attributes, bw);
            StringUtil.Serialize(caption, bw);
            if ((flags & 1) != 0)
                ObjectUtils.SerializeObject(stickers, bw);
            if ((flags & 2) != 0)
                bw.Write(ttl_seconds.Value);

        }
    }
}
