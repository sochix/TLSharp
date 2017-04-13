using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-1240849242)]
    public class TLStickerSet : TLObject
    {
        public override int Constructor => -1240849242;

        public TLStickerSet set { get; set; }
        public TLVector<TLStickerPack> packs { get; set; }
        public TLVector<TLAbsDocument> documents { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            set = (TLStickerSet) ObjectUtils.DeserializeObject(br);
            packs = ObjectUtils.DeserializeVector<TLStickerPack>(br);
            documents = ObjectUtils.DeserializeVector<TLAbsDocument>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(set, bw);
            ObjectUtils.SerializeObject(packs, bw);
            ObjectUtils.SerializeObject(documents, bw);
        }
    }
}