using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1632839530)]
    public class TLChatPhoto : TLAbsChatPhoto
    {
        public override int Constructor => 1632839530;

        public TLAbsFileLocation photo_small { get; set; }
        public TLAbsFileLocation photo_big { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            photo_small = (TLAbsFileLocation) ObjectUtils.DeserializeObject(br);
            photo_big = (TLAbsFileLocation) ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(photo_small, bw);
            ObjectUtils.SerializeObject(photo_big, bw);
        }
    }
}