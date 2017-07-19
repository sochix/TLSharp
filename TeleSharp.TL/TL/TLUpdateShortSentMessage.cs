using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(301019932)]
    public class TLUpdateShortSentMessage : TLAbsUpdates
    {
        public override int Constructor
        {
            get
            {
                return 301019932;
            }
        }

        public int flags { get; set; }
        public bool @out { get; set; }
        public int id { get; set; }
        public int pts { get; set; }
        public int pts_count { get; set; }
        public int date { get; set; }
        public TLAbsMessageMedia media { get; set; }
        public TLVector<TLAbsMessageEntity> entities { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = @out ? (flags | 2) : (flags & ~2);
            flags = media != null ? (flags | 512) : (flags & ~512);
            flags = entities != null ? (flags | 128) : (flags & ~128);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            @out = (flags & 2) != 0;
            id = br.ReadInt32();
            pts = br.ReadInt32();
            pts_count = br.ReadInt32();
            date = br.ReadInt32();
            if ((flags & 512) != 0)
                media = (TLAbsMessageMedia)ObjectUtils.DeserializeObject(br);
            else
                media = null;

            if ((flags & 128) != 0)
                entities = (TLVector<TLAbsMessageEntity>)ObjectUtils.DeserializeVector<TLAbsMessageEntity>(br);
            else
                entities = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            bw.Write(id);
            bw.Write(pts);
            bw.Write(pts_count);
            bw.Write(date);
            if ((flags & 512) != 0)
                ObjectUtils.SerializeObject(media, bw);
            if ((flags & 128) != 0)
                ObjectUtils.SerializeObject(entities, bw);

        }
    }
}
