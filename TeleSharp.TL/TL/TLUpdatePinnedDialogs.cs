using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-657787251)]
    public class TLUpdatePinnedDialogs : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -657787251;
            }
        }

        public int flags { get; set; }
        public TLVector<TLAbsPeer> order { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = order != null ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            if ((flags & 1) != 0)
                order = (TLVector<TLAbsPeer>)ObjectUtils.DeserializeVector<TLAbsPeer>(br);
            else
                order = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            if ((flags & 1) != 0)
                ObjectUtils.SerializeObject(order, bw);

        }
    }
}
