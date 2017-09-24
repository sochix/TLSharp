using System.IO;

namespace TeleSharp.TL.Payments
{
    [TLObject(-1601001088)]
    public class TLRequestGetPaymentReceipt : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1601001088;
            }
        }

        public int msg_id { get; set; }
        public Payments.TLPaymentReceipt Response { get; set; }

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
            Response = (Payments.TLPaymentReceipt)ObjectUtils.DeserializeObject(br);
        }
    }
}