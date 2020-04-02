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

        public int Flags { get; set; }
        public bool Masks { get; set; }
        public TLVector<long> Order { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Masks ? (Flags | 1) : (Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Masks = (Flags & 1) != 0;
            Order = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);

            ObjectUtils.SerializeObject(Order, bw);

        }
    }
}
