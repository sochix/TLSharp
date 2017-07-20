using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1892568281)]
    public class TLMessageActionPaymentSentMe : TLAbsMessageAction
    {
        public override int Constructor
        {
            get
            {
                return -1892568281;
            }
        }

        public int flags { get; set; }
        public string currency { get; set; }
        public long total_amount { get; set; }
        public byte[] payload { get; set; }
        public TLPaymentRequestedInfo info { get; set; }
        public string shipping_option_id { get; set; }
        public TLPaymentCharge charge { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = info != null ? (flags | 1) : (flags & ~1);
            flags = shipping_option_id != null ? (flags | 2) : (flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            currency = StringUtil.Deserialize(br);
            total_amount = br.ReadInt64();
            payload = BytesUtil.Deserialize(br);
            if ((flags & 1) != 0)
                info = (TLPaymentRequestedInfo)ObjectUtils.DeserializeObject(br);
            else
                info = null;

            if ((flags & 2) != 0)
                shipping_option_id = StringUtil.Deserialize(br);
            else
                shipping_option_id = null;

            charge = (TLPaymentCharge)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            StringUtil.Serialize(currency, bw);
            bw.Write(total_amount);
            BytesUtil.Serialize(payload, bw);
            if ((flags & 1) != 0)
                ObjectUtils.SerializeObject(info, bw);
            if ((flags & 2) != 0)
                StringUtil.Serialize(shipping_option_id, bw);
            ObjectUtils.SerializeObject(charge, bw);

        }
    }
}
