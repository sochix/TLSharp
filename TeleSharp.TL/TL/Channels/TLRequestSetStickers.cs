using System.IO;
namespace TeleSharp.TL.Channels
{
    [TLObject(-359881479)]
    public class TLRequestSetStickers : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -359881479;
            }
        }

        public TLAbsInputChannel channel { get; set; }
        public TLAbsInputStickerSet stickerset { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            stickerset = (TLAbsInputStickerSet)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel, bw);
            ObjectUtils.SerializeObject(stickerset, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
