using System.IO;

namespace TeleSharp.TL.Stickers
{
    [TLObject(-4795190)]
    public class TLRequestChangeStickerPosition : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -4795190;
            }
        }

        public int Position { get; set; }

        public Messages.TLStickerSet Response { get; set; }

        public TLAbsInputDocument Sticker { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Sticker = (TLAbsInputDocument)ObjectUtils.DeserializeObject(br);
            Position = br.ReadInt32();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLStickerSet)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Sticker, bw);
            bw.Write(Position);
        }
    }
}
