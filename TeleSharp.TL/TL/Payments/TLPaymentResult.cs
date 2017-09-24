using System.IO;

namespace TeleSharp.TL.Payments
{
    [TLObject(1314881805)]
    public class TLPaymentResult : TLAbsPaymentResult
    {
        public override int Constructor
        {
            get
            {
                return 1314881805;
            }
        }

        public TLAbsUpdates updates { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            updates = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(updates, bw);
        }
    }
}