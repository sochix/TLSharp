using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-947462709)]
    public class TLMessageFwdHeader : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -947462709;
            }
        }

        public int flags { get; set; }
        public int? from_id { get; set; }
        public int date { get; set; }
        public int? channel_id { get; set; }
        public int? channel_post { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = from_id != null ? (flags | 1) : (flags & ~1);
            flags = channel_id != null ? (flags | 2) : (flags & ~2);
            flags = channel_post != null ? (flags | 4) : (flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            if ((flags & 1) != 0)
                from_id = br.ReadInt32();
            else
                from_id = null;

            date = br.ReadInt32();
            if ((flags & 2) != 0)
                channel_id = br.ReadInt32();
            else
                channel_id = null;

            if ((flags & 4) != 0)
                channel_post = br.ReadInt32();
            else
                channel_post = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            if ((flags & 1) != 0)
                bw.Write(from_id.Value);
            bw.Write(date);
            if ((flags & 2) != 0)
                bw.Write(channel_id.Value);
            if ((flags & 4) != 0)
                bw.Write(channel_post.Value);

        }
    }
}
