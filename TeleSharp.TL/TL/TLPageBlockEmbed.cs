using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-840826671)]
    public class TLPageBlockEmbed : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return -840826671;
            }
        }

        public int flags { get; set; }
        public bool full_width { get; set; }
        public bool allow_scrolling { get; set; }
        public string url { get; set; }
        public string html { get; set; }
        public long? poster_photo_id { get; set; }
        public int w { get; set; }
        public int h { get; set; }
        public TLAbsRichText caption { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = full_width ? (flags | 1) : (flags & ~1);
            flags = allow_scrolling ? (flags | 8) : (flags & ~8);
            flags = url != null ? (flags | 2) : (flags & ~2);
            flags = html != null ? (flags | 4) : (flags & ~4);
            flags = poster_photo_id != null ? (flags | 16) : (flags & ~16);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            full_width = (flags & 1) != 0;
            allow_scrolling = (flags & 8) != 0;
            if ((flags & 2) != 0)
                url = StringUtil.Deserialize(br);
            else
                url = null;

            if ((flags & 4) != 0)
                html = StringUtil.Deserialize(br);
            else
                html = null;

            if ((flags & 16) != 0)
                poster_photo_id = br.ReadInt64();
            else
                poster_photo_id = null;

            w = br.ReadInt32();
            h = br.ReadInt32();
            caption = (TLAbsRichText)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);


            if ((flags & 2) != 0)
                StringUtil.Serialize(url, bw);
            if ((flags & 4) != 0)
                StringUtil.Serialize(html, bw);
            if ((flags & 16) != 0)
                bw.Write(poster_photo_id.Value);
            bw.Write(w);
            bw.Write(h);
            ObjectUtils.SerializeObject(caption, bw);

        }
    }
}
