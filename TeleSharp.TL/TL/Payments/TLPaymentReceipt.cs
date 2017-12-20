using System.IO;

namespace TeleSharp.TL.Payments
{
    [TLObject(1342771681)]
    public class TLPaymentReceipt : TLObject
    {
        public int BotId { get; set; }

        public override int Constructor
        {
            get
            {
                return 1342771681;
            }
        }

        public string CredentialsTitle { get; set; }

        public string Currency { get; set; }

        public int Date { get; set; }

        public int Flags { get; set; }

        public TLPaymentRequestedInfo Info { get; set; }

        public TLInvoice Invoice { get; set; }

        public int ProviderId { get; set; }

        public TLShippingOption Shipping { get; set; }

        public long TotalAmount { get; set; }

        public TLVector<TLAbsUser> Users { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Date = br.ReadInt32();
            BotId = br.ReadInt32();
            Invoice = (TLInvoice)ObjectUtils.DeserializeObject(br);
            ProviderId = br.ReadInt32();
            if ((Flags & 1) != 0)
                Info = (TLPaymentRequestedInfo)ObjectUtils.DeserializeObject(br);
            else
                Info = null;

            if ((Flags & 2) != 0)
                Shipping = (TLShippingOption)ObjectUtils.DeserializeObject(br);
            else
                Shipping = null;

            Currency = StringUtil.Deserialize(br);
            TotalAmount = br.ReadInt64();
            CredentialsTitle = StringUtil.Deserialize(br);
            Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            bw.Write(Date);
            bw.Write(BotId);
            ObjectUtils.SerializeObject(Invoice, bw);
            bw.Write(ProviderId);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(Info, bw);
            if ((Flags & 2) != 0)
                ObjectUtils.SerializeObject(Shipping, bw);
            StringUtil.Serialize(Currency, bw);
            bw.Write(TotalAmount);
            StringUtil.Serialize(CredentialsTitle, bw);
            ObjectUtils.SerializeObject(Users, bw);
        }
    }
}
