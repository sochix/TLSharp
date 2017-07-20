using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(-1764049896)]
    public class TLNoPassword : TLAbsPassword
    {
        public override int Constructor
        {
            get
            {
                return -1764049896;
            }
        }

        public byte[] new_salt { get; set; }
        public string email_unconfirmed_pattern { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            new_salt = BytesUtil.Deserialize(br);
            email_unconfirmed_pattern = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(new_salt, bw);
            StringUtil.Serialize(email_unconfirmed_pattern, bw);

        }
    }
}
