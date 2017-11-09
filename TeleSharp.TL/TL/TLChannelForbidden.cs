using System.IO;
namespace TeleSharp.TL
{
    [TLObject(681420594)]
    public class TLChannelForbidden : TLAbsChat
    {
        public override int Constructor
        {
            get
            {
                return 681420594;
            }
        }

        public int flags { get; set; }
        public bool broadcast { get; set; }
        public bool megagroup { get; set; }
        public int id { get; set; }
        public long access_hash { get; set; }
        public string title { get; set; }
        public int? until_date { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = broadcast ? (flags | 32) : (flags & ~32);
            flags = megagroup ? (flags | 256) : (flags & ~256);
            flags = until_date != null ? (flags | 65536) : (flags & ~65536);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            broadcast = (flags & 32) != 0;
            megagroup = (flags & 256) != 0;
            id = br.ReadInt32();
            access_hash = br.ReadInt64();
            title = StringUtil.Deserialize(br);
            if ((flags & 65536) != 0)
                until_date = br.ReadInt32();
            else
                until_date = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);


            bw.Write(id);
            bw.Write(access_hash);
            StringUtil.Serialize(title, bw);
            if ((flags & 65536) != 0)
                bw.Write(until_date.Value);

        }
    }
}
