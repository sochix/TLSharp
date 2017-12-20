using System.IO;

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
            bw.Write(Flags);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(Order, bw);
        }
    }
}
