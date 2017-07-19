using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-368917890)]
    public class TLPaymentCharge : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -368917890;
            }
        }

        public string id { get; set; }
        public string provider_charge_id { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = StringUtil.Deserialize(br);
            provider_charge_id = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(id, bw);
            StringUtil.Serialize(provider_charge_id, bw);

        }
    }
}
