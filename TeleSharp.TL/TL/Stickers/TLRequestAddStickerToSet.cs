using System.IO;

namespace TeleSharp.TL.Stickers
{
    [TLObject(-2041315650)]
    public class TLRequestAddStickerToSet : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -2041315650;
            }
        }

        public Messages.TLStickerSet Response { get; set; }

        public TLInputStickerSetItem Sticker { get; set; }

        public TLAbsInputStickerSet Stickerset { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Stickerset = (TLAbsInputStickerSet)ObjectUtils.DeserializeObject(br);
            Sticker = (TLInputStickerSetItem)ObjectUtils.DeserializeObject(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLStickerSet)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Stickerset, bw);
            ObjectUtils.SerializeObject(Sticker, bw);
        }
    }
}
