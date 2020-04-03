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

        public int Flags { get; set; }
        public bool Test { get; set; }
        public bool NameRequested { get; set; }
        public bool PhoneRequested { get; set; }
        public bool EmailRequested { get; set; }
        public bool ShippingAddressRequested { get; set; }
        public bool Flexible { get; set; }
        public bool PhoneToProvider { get; set; }
        public bool EmailToProvider { get; set; }
        public string Currency { get; set; }
        public TLVector<TLLabeledPrice> Prices { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Test = (Flags & 1) != 0;
            NameRequested = (Flags & 2) != 0;
            PhoneRequested = (Flags & 4) != 0;
            EmailRequested = (Flags & 8) != 0;
            ShippingAddressRequested = (Flags & 16) != 0;
            Flexible = (Flags & 32) != 0;
            PhoneToProvider = (Flags & 64) != 0;
            EmailToProvider = (Flags & 128) != 0;
            Currency = StringUtil.Deserialize(br);
            Prices = (TLVector<TLLabeledPrice>)ObjectUtils.DeserializeVector<TLLabeledPrice>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);








            StringUtil.Serialize(Currency, bw);
            ObjectUtils.SerializeObject(Prices, bw);

        }
    }
}
