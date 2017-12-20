using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-512463606)]
    public class TLInputReportReasonOther : TLAbsReportReason
    {
        public override int Constructor
        {
            get
            {
                return -512463606;
            }
        }

        public string Text { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Text = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Text, bw);
        }
    }
}
