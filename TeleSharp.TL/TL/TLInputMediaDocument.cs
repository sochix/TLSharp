using System.IO;
namespace TeleSharp.TL
{
    [TLObject(1523279502)]
    public class TLInputMediaDocument : TLAbsInputMedia
    {
        public override int Constructor
        {
            get
            {
                return 1523279502;
            }
        }

        public int flags { get; set; }
        public TLAbsInputDocument id { get; set; }
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
            id = (TLAbsInputDocument)ObjectUtils.DeserializeObject(br);
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
            ObjectUtils.SerializeObject(id, bw);
            StringUtil.Serialize(caption, bw);
            if ((flags & 1) != 0)
                bw.Write(ttl_seconds.Value);

        }
    }
}
