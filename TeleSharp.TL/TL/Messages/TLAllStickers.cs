using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-302170017)]
    public class TLAllStickers : TLAbsAllStickers
    {
        public override int Constructor => -302170017;

        public int hash { get; set; }
        public TLVector<TLStickerSet> sets { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            hash = br.ReadInt32();
            sets = ObjectUtils.DeserializeVector<TLStickerSet>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(hash);
            ObjectUtils.SerializeObject(sets, bw);
        }
    }
}