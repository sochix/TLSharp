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

        public int MsgId { get; set; }

        public Payments.TLPaymentReceipt Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            MsgId = br.ReadInt32();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Payments.TLPaymentReceipt)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(MsgId);
        }
    }
}
