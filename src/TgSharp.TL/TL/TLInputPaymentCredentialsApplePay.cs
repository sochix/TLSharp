using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(178373535)]
    public class TLInputPaymentCredentialsApplePay : TLAbsInputPaymentCredentials
    {
        public override int Constructor
        {
            get
            {
                return 178373535;
            }
        }

        public TLDataJSON PaymentData { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            PaymentData = (TLDataJSON)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(PaymentData, bw);
        }
    }
}
