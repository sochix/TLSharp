using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(639215886)]
    public class TLRequestGetStickerSet : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 639215886;
            }
        }

        public Messages.TLStickerSet Response { get; set; }

        public TLAbsInputStickerSet Stickerset { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Stickerset = (TLAbsInputStickerSet)ObjectUtils.DeserializeObject(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLStickerSet)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Stickerset, bw);
        }
    }
}
