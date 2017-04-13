using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1032643901)]
    public class TLMessageMediaPhoto : TLAbsMessageMedia
    {
        public override int Constructor => 1032643901;

        public TLAbsPhoto photo { get; set; }
        public string caption { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            photo = (TLAbsPhoto) ObjectUtils.DeserializeObject(br);
            caption = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(photo, bw);
            StringUtil.Serialize(caption, bw);
        }
    }
}