using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(639215886)]
    public class TLRequestGetStickerSet : TLMethod
    {
        public override int Constructor => 639215886;

        public TLAbsInputStickerSet stickerset { get; set; }
        public TLStickerSet Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            stickerset = (TLAbsInputStickerSet) ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(stickerset, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLStickerSet) ObjectUtils.DeserializeObject(br);
        }
    }
}