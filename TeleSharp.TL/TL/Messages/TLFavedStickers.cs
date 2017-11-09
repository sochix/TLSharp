using System.IO;
namespace TeleSharp.TL.Messages
{
    [TLObject(-209768682)]
    public class TLFavedStickers : TLAbsFavedStickers
    {
        public override int Constructor
        {
            get
            {
                return -209768682;
            }
        }

        public int hash { get; set; }
        public TLVector<TLStickerPack> packs { get; set; }
        public TLVector<TLAbsDocument> stickers { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            hash = br.ReadInt32();
            packs = (TLVector<TLStickerPack>)ObjectUtils.DeserializeVector<TLStickerPack>(br);
            stickers = (TLVector<TLAbsDocument>)ObjectUtils.DeserializeVector<TLAbsDocument>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(hash);
            ObjectUtils.SerializeObject(packs, bw);
            ObjectUtils.SerializeObject(stickers, bw);

        }
    }
}
