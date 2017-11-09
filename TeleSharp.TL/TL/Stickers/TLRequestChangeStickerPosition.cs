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

        public TLAbsInputDocument sticker { get; set; }
        public int position { get; set; }
        public Messages.TLStickerSet Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            sticker = (TLAbsInputDocument)ObjectUtils.DeserializeObject(br);
            position = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(sticker, bw);
            bw.Write(position);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLStickerSet)ObjectUtils.DeserializeObject(br);

        }
    }
}
