using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-1312568665)]
    public class TLChannelAdminLogEventActionChangeStickerSet : TLAbsChannelAdminLogEventAction
    {
        public override int Constructor
        {
            get
            {
                return -1312568665;
            }
        }

        public TLAbsInputStickerSet prev_stickerset { get; set; }
        public TLAbsInputStickerSet new_stickerset { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            prev_stickerset = (TLAbsInputStickerSet)ObjectUtils.DeserializeObject(br);
            new_stickerset = (TLAbsInputStickerSet)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(prev_stickerset, bw);
            ObjectUtils.SerializeObject(new_stickerset, bw);

        }
    }
}
