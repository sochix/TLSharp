using System.IO;
namespace TeleSharp.TL
{
    [TLObject(792191537)]
    public class TLInputMediaUploadedPhoto : TLAbsInputMedia
    {
        public override int Constructor
        {
            get
            {
                return 792191537;
            }
        }

        public int flags { get; set; }
        public TLAbsInputFile file { get; set; }
        public string caption { get; set; }
        public TLVector<TLAbsInputDocument> stickers { get; set; }
        public int? ttl_seconds { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = stickers != null ? (flags | 1) : (flags & ~1);
            flags = ttl_seconds != null ? (flags | 2) : (flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            file = (TLAbsInputFile)ObjectUtils.DeserializeObject(br);
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
            StringUtil.Serialize(caption, bw);
            if ((flags & 1) != 0)
                ObjectUtils.SerializeObject(stickers, bw);
            if ((flags & 2) != 0)
                bw.Write(ttl_seconds.Value);

        }
    }
}
