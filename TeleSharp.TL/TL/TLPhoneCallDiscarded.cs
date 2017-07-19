using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1355435489)]
    public class TLPhoneCallDiscarded : TLAbsPhoneCall
    {
        public override int Constructor
        {
            get
            {
                return 1355435489;
            }
        }

        public int flags { get; set; }
        public bool need_rating { get; set; }
        public bool need_debug { get; set; }
        public long id { get; set; }
        public TLAbsPhoneCallDiscardReason reason { get; set; }
        public int? duration { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = need_rating ? (flags | 4) : (flags & ~4);
            flags = need_debug ? (flags | 8) : (flags & ~8);
            flags = reason != null ? (flags | 1) : (flags & ~1);
            flags = duration != null ? (flags | 2) : (flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            need_rating = (flags & 4) != 0;
            need_debug = (flags & 8) != 0;
            id = br.ReadInt64();
            if ((flags & 1) != 0)
                reason = (TLAbsPhoneCallDiscardReason)ObjectUtils.DeserializeObject(br);
            else
                reason = null;

            if ((flags & 2) != 0)
                duration = br.ReadInt32();
            else
                duration = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);


            bw.Write(id);
            if ((flags & 1) != 0)
                ObjectUtils.SerializeObject(reason, bw);
            if ((flags & 2) != 0)
                bw.Write(duration.Value);

        }
    }
}
