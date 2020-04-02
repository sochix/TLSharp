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

        public int Flags { get; set; }
        public TLVector<TLAbsPeer> Order { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Order != null ? (Flags | 1) : (Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            if ((Flags & 1) != 0)
                Order = (TLVector<TLAbsPeer>)ObjectUtils.DeserializeVector<TLAbsPeer>(br);
            else
                Order = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(Order, bw);

        }
    }
}
