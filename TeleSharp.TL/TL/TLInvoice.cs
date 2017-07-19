using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1022713000)]
    public class TLInvoice : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1022713000;
            }
        }

        public int flags { get; set; }
        public bool test { get; set; }
        public bool name_requested { get; set; }
        public bool phone_requested { get; set; }
        public bool email_requested { get; set; }
        public bool shipping_address_requested { get; set; }
        public bool flexible { get; set; }
        public string currency { get; set; }
        public TLVector<TLLabeledPrice> prices { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = test ? (flags | 1) : (flags & ~1);
            flags = name_requested ? (flags | 2) : (flags & ~2);
            flags = phone_requested ? (flags | 4) : (flags & ~4);
            flags = email_requested ? (flags | 8) : (flags & ~8);
            flags = shipping_address_requested ? (flags | 16) : (flags & ~16);
            flags = flexible ? (flags | 32) : (flags & ~32);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            test = (flags & 1) != 0;
            name_requested = (flags & 2) != 0;
            phone_requested = (flags & 4) != 0;
            email_requested = (flags & 8) != 0;
            shipping_address_requested = (flags & 16) != 0;
            flexible = (flags & 32) != 0;
            currency = StringUtil.Deserialize(br);
            prices = (TLVector<TLLabeledPrice>)ObjectUtils.DeserializeVector<TLLabeledPrice>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);






            StringUtil.Serialize(currency, bw);
            ObjectUtils.SerializeObject(prices, bw);

        }
    }
}
