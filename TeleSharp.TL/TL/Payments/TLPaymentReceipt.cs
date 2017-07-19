using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Payments
{
    [TLObject(1342771681)]
    public class TLPaymentReceipt : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1342771681;
            }
        }

        public int flags { get; set; }
        public int date { get; set; }
        public int bot_id { get; set; }
        public TLInvoice invoice { get; set; }
        public int provider_id { get; set; }
        public TLPaymentRequestedInfo info { get; set; }
        public TLShippingOption shipping { get; set; }
        public string currency { get; set; }
        public long total_amount { get; set; }
        public string credentials_title { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = info != null ? (flags | 1) : (flags & ~1);
            flags = shipping != null ? (flags | 2) : (flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            date = br.ReadInt32();
            bot_id = br.ReadInt32();
            invoice = (TLInvoice)ObjectUtils.DeserializeObject(br);
            provider_id = br.ReadInt32();
            if ((flags & 1) != 0)
                info = (TLPaymentRequestedInfo)ObjectUtils.DeserializeObject(br);
            else
                info = null;

            if ((flags & 2) != 0)
                shipping = (TLShippingOption)ObjectUtils.DeserializeObject(br);
            else
                shipping = null;

            currency = StringUtil.Deserialize(br);
            total_amount = br.ReadInt64();
            credentials_title = StringUtil.Deserialize(br);
            users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            bw.Write(date);
            bw.Write(bot_id);
            ObjectUtils.SerializeObject(invoice, bw);
            bw.Write(provider_id);
            if ((flags & 1) != 0)
                ObjectUtils.SerializeObject(info, bw);
            if ((flags & 2) != 0)
                ObjectUtils.SerializeObject(shipping, bw);
            StringUtil.Serialize(currency, bw);
            bw.Write(total_amount);
            StringUtil.Serialize(credentials_title, bw);
            ObjectUtils.SerializeObject(users, bw);

        }
    }
}
