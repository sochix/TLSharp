using System.IO;

namespace TeleSharp.TL
{
    [TLObject(872932635)]
    public class TLStickerSetMultiCovered : TLAbsStickerSetCovered
    {
        public override int Constructor => 872932635;

        public TLStickerSet set { get; set; }
        public TLVector<TLAbsDocument> covers { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            set = (TLStickerSet) ObjectUtils.DeserializeObject(br);
            covers = ObjectUtils.DeserializeVector<TLAbsDocument>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(set, bw);
            ObjectUtils.SerializeObject(covers, bw);
        }
    }
}