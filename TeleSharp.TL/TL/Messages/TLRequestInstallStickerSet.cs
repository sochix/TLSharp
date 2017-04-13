using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-946871200)]
    public class TLRequestInstallStickerSet : TLMethod
    {
        public override int Constructor => -946871200;

        public TLAbsInputStickerSet stickerset { get; set; }
        public bool archived { get; set; }
        public TLAbsStickerSetInstallResult Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            stickerset = (TLAbsInputStickerSet) ObjectUtils.DeserializeObject(br);
            archived = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(stickerset, bw);
            BoolUtil.Serialize(archived, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsStickerSetInstallResult) ObjectUtils.DeserializeObject(br);
        }
    }
}