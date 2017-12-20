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

        public TLAbsInputStickerSet NewStickerset { get; set; }

        public TLAbsInputStickerSet PrevStickerset { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            PrevStickerset = (TLAbsInputStickerSet)ObjectUtils.DeserializeObject(br);
            NewStickerset = (TLAbsInputStickerSet)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(PrevStickerset, bw);
            ObjectUtils.SerializeObject(NewStickerset, bw);
        }
    }
}
