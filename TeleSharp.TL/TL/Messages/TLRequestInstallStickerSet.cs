using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-946871200)]
    public class TLRequestInstallStickerSet : TLMethod
    {
        public bool Archived { get; set; }

        public override int Constructor
        {
            get
            {
                return -946871200;
            }
        }

        public Messages.TLAbsStickerSetInstallResult Response { get; set; }

        public TLAbsInputStickerSet Stickerset { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Stickerset = (TLAbsInputStickerSet)ObjectUtils.DeserializeObject(br);
            Archived = BoolUtil.Deserialize(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAbsStickerSetInstallResult)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Stickerset, bw);
            BoolUtil.Serialize(Archived, bw);
        }
    }
}
