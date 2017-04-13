using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-512463606)]
    public class TLInputReportReasonOther : TLAbsReportReason
    {
        public override int Constructor => -512463606;

        public string text { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            text = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(text, bw);
        }
    }
}