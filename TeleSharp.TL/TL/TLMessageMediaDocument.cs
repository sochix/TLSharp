using System.IO;
namespace TeleSharp.TL
{
    [TLObject(2084836563)]
    public class TLMessageMediaDocument : TLAbsMessageMedia
    {
        public override int Constructor
        {
            get
            {
                return 2084836563;
            }
        }

        public int flags { get; set; }
        public TLAbsDocument document { get; set; }
        public string caption { get; set; }
        public int? ttl_seconds { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = document != null ? (flags | 1) : (flags & ~1);
            flags = caption != null ? (flags | 2) : (flags & ~2);
            flags = ttl_seconds != null ? (flags | 4) : (flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            if ((flags & 1) != 0)
                document = (TLAbsDocument)ObjectUtils.DeserializeObject(br);
            else
                document = null;

            if ((flags & 2) != 0)
                caption = StringUtil.Deserialize(br);
            else
                caption = null;

            if ((flags & 4) != 0)
                ttl_seconds = br.ReadInt32();
            else
                ttl_seconds = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            if ((flags & 1) != 0)
                ObjectUtils.SerializeObject(document, bw);
            if ((flags & 2) != 0)
                StringUtil.Serialize(caption, bw);
            if ((flags & 4) != 0)
                bw.Write(ttl_seconds.Value);

        }
    }
}
