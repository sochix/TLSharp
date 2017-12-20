using System.IO;

namespace TeleSharp.TL
{
    [TLObject(505595789)]
    public class TLInputReportReasonViolence : TLAbsReportReason
    {
        public override int Constructor
        {
            get
            {
                return 505595789;
            }
        }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
        }
    }
}
