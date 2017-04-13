using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-123893531)]
    public class TLFeaturedStickers : TLAbsFeaturedStickers
    {
        public override int Constructor => -123893531;

        public int hash { get; set; }
        public TLVector<TLAbsStickerSetCovered> sets { get; set; }
        public TLVector<long> unread { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            hash = br.ReadInt32();
            sets = ObjectUtils.DeserializeVector<TLAbsStickerSetCovered>(br);
            unread = ObjectUtils.DeserializeVector<long>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(hash);
            ObjectUtils.SerializeObject(sets, bw);
            ObjectUtils.SerializeObject(unread, bw);
        }
    }
}