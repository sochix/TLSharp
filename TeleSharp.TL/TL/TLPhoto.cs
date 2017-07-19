using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1836524247)]
    public class TLPhoto : TLAbsPhoto
    {
        public override int Constructor
        {
            get
            {
                return -1836524247;
            }
        }

        public int flags { get; set; }
        public bool has_stickers { get; set; }
        public long id { get; set; }
        public long access_hash { get; set; }
        public int date { get; set; }
        public TLVector<TLAbsPhotoSize> sizes { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = has_stickers ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            has_stickers = (flags & 1) != 0;
            id = br.ReadInt64();
            access_hash = br.ReadInt64();
            date = br.ReadInt32();
            sizes = (TLVector<TLAbsPhotoSize>)ObjectUtils.DeserializeVector<TLAbsPhotoSize>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            bw.Write(id);
            bw.Write(access_hash);
            bw.Write(date);
            ObjectUtils.SerializeObject(sizes, bw);

        }
    }
}
