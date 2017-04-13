using System.IO;

namespace TeleSharp.TL
{
    [TLObject(777640226)]
    public class TLInputReportReasonPornography : TLAbsReportReason
    {
        public override int Constructor => 777640226;


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