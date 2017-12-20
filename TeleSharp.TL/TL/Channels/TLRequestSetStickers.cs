using System.IO;

namespace TeleSharp.TL.Channels
{
    [TLObject(-359881479)]
    public class TLRequestSetStickers : TLMethod
    {
        public TLAbsInputChannel Channel { get; set; }

        public override int Constructor
        {
            get
            {
                return -359881479;
            }
        }

        public bool Response { get; set; }

        public TLAbsInputStickerSet Stickerset { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            Stickerset = (TLAbsInputStickerSet)ObjectUtils.DeserializeObject(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Channel, bw);
            ObjectUtils.SerializeObject(Stickerset, bw);
        }
    }
}
