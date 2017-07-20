using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
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

        public string id { get; set; }
        public byte[] tmp_password { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = StringUtil.Deserialize(br);
            tmp_password = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(id, bw);
            BytesUtil.Serialize(tmp_password, bw);

        }
    }
}
