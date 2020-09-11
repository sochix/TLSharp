using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(-905587442)]
    public class TLInputPaymentCredentialsAndroidPay : TLAbsInputPaymentCredentials
    {
        public override int Constructor
        {
            get
            {
                return -905587442;
            }
        }

        public TLDataJSON PaymentToken { get; set; }
        public string GoogleTransactionId { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            PaymentToken = (TLDataJSON)ObjectUtils.DeserializeObject(br);
            GoogleTransactionId = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(PaymentToken, bw);
            StringUtil.Serialize(GoogleTransactionId, bw);
        }
    }
}
