using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-373312269)]
    public class TLInputMediaPhoto : TLAbsInputMedia
    {
        public override int Constructor => -373312269;

        public TLAbsInputPhoto id { get; set; }
        public string caption { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = (TLAbsInputPhoto) ObjectUtils.DeserializeObject(br);
            caption = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(id, bw);
            StringUtil.Serialize(caption, bw);
        }
    }
}