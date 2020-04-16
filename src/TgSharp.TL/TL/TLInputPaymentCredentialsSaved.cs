using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(-1056001329)]
    public class TLInputPaymentCredentialsSaved : TLAbsInputPaymentCredentials
    {
        public override int Constructor
        {
            get
            {
                return -1056001329;
            }
        }

        public string Id { get; set; }
        public byte[] TmpPassword { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = StringUtil.Deserialize(br);
            TmpPassword = BytesUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Id, bw);
            BytesUtil.Serialize(TmpPassword, bw);
        }
    }
}
