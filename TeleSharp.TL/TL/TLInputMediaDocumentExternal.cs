using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-1225309387)]
    public class TLInputMediaDocumentExternal : TLAbsInputMedia
    {
        public override int Constructor
        {
            get
            {
                return -1225309387;
            }
        }

        public int flags { get; set; }
        public string url { get; set; }
        public string caption { get; set; }
        public int? ttl_seconds { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = ttl_seconds != null ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            url = StringUtil.Deserialize(br);
            caption = StringUtil.Deserialize(br);
            if ((flags & 1) != 0)
                ttl_seconds = br.ReadInt32();
            else
                ttl_seconds = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            StringUtil.Serialize(url, bw);
            StringUtil.Serialize(caption, bw);
            if ((flags & 1) != 0)
                bw.Write(ttl_seconds.Value);

        }
    }
}
