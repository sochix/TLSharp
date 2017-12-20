using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1892568281)]
    public class TLMessageActionPaymentSentMe : TLAbsMessageAction
    {
        public TLPaymentCharge Charge { get; set; }

        public override int Constructor
        {
            get
            {
                return -1892568281;
            }
        }

        public string Currency { get; set; }

        public int Flags { get; set; }

        public TLPaymentRequestedInfo Info { get; set; }

        public byte[] Payload { get; set; }

        public string ShippingOptionId { get; set; }

        public long TotalAmount { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Currency = StringUtil.Deserialize(br);
            TotalAmount = br.ReadInt64();
            Payload = BytesUtil.Deserialize(br);
            if ((Flags & 1) != 0)
                Info = (TLPaymentRequestedInfo)ObjectUtils.DeserializeObject(br);
            else
                Info = null;

            if ((Flags & 2) != 0)
                ShippingOptionId = StringUtil.Deserialize(br);
            else
                ShippingOptionId = null;

            Charge = (TLPaymentCharge)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            StringUtil.Serialize(Currency, bw);
            bw.Write(TotalAmount);
            BytesUtil.Serialize(Payload, bw);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(Info, bw);
            if ((Flags & 2) != 0)
                StringUtil.Serialize(ShippingOptionId, bw);
            ObjectUtils.SerializeObject(Charge, bw);
        }
    }
}
