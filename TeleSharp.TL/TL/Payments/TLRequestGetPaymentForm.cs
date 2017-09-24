using System.IO;

namespace TeleSharp.TL.Payments
{
    [TLObject(-1712285883)]
    public class TLRequestGetPaymentForm : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1712285883;
            }
        }

        public int msg_id { get; set; }
        public Payments.TLPaymentForm Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            msg_id = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(msg_id);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Payments.TLPaymentForm)ObjectUtils.DeserializeObject(br);
        }
    }
}