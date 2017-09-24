using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-203411800)]
    public class TLMessageMediaDocument : TLAbsMessageMedia
    {
        public override int Constructor
        {
            get
            {
                return -203411800;
            }
        }

        public TLAbsDocument document { get; set; }
        public string caption { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            document = (TLAbsDocument)ObjectUtils.DeserializeObject(br);
            caption = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(document, bw);
            StringUtil.Serialize(caption, bw);
        }
    }
}