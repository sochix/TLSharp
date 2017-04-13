using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1490799288)]
    public class TLInputReportReasonSpam : TLAbsReportReason
    {
        public override int Constructor => 1490799288;


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