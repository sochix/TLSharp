using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-866424884)]
    public class TLRequestGetAttachedStickers : TLMethod
    {
        public override int Constructor => -866424884;

        public TLAbsInputStickeredMedia media { get; set; }
        public TLVector<TLAbsStickerSetCovered> Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            media = (TLAbsInputStickeredMedia) ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(media, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = ObjectUtils.DeserializeVector<TLAbsStickerSetCovered>(br);
        }
    }
}