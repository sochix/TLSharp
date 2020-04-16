using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
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

        public string Id { get; set; }
        public string ProviderChargeId { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = StringUtil.Deserialize(br);
            ProviderChargeId = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Id, bw);
            StringUtil.Serialize(ProviderChargeId, bw);
        }
    }
}
