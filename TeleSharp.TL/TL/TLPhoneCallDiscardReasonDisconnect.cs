using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-527056480)]
    public class TLPhoneCallDiscardReasonDisconnect : TLAbsPhoneCallDiscardReason
    {
        public override int Constructor
        {
            get
            {
                return -527056480;
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