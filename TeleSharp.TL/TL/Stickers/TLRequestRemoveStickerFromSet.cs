using System.IO;

namespace TeleSharp.TL.Stickers
{
    [TLObject(-143257775)]
    public class TLRequestRemoveStickerFromSet : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -143257775;
            }
        }

        public Messages.TLStickerSet Response { get; set; }

        public TLAbsInputDocument Sticker { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Sticker = (TLAbsInputDocument)ObjectUtils.DeserializeObject(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLStickerSet)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Sticker, bw);
        }
    }
}
