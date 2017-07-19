using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(196268545)]
    public class TLUpdateStickerSetsOrder : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 196268545;
            }
        }

        public int flags { get; set; }
        public bool masks { get; set; }
        public TLVector<long> order { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = masks ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            masks = (flags & 1) != 0;
            order = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            ObjectUtils.SerializeObject(order, bw);

        }
    }
}
