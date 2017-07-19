using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1080663248)]
    public class TLMessageActionPaymentSent : TLAbsMessageAction
    {
        public override int Constructor
        {
            get
            {
                return 1080663248;
            }
        }

        public string currency { get; set; }
        public long total_amount { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            currency = StringUtil.Deserialize(br);
            total_amount = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(currency, bw);
            bw.Write(total_amount);

        }
    }
}
