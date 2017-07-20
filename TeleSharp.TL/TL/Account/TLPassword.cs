using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(2081952796)]
    public class TLPassword : TLAbsPassword
    {
        public override int Constructor
        {
            get
            {
                return 2081952796;
            }
        }

        public byte[] current_salt { get; set; }
        public byte[] new_salt { get; set; }
        public string hint { get; set; }
        public bool has_recovery { get; set; }
        public string email_unconfirmed_pattern { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            current_salt = BytesUtil.Deserialize(br);
            new_salt = BytesUtil.Deserialize(br);
            hint = StringUtil.Deserialize(br);
            has_recovery = BoolUtil.Deserialize(br);
            email_unconfirmed_pattern = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(current_salt, bw);
            BytesUtil.Serialize(new_salt, bw);
            StringUtil.Serialize(hint, bw);
            BoolUtil.Serialize(has_recovery, bw);
            StringUtil.Serialize(email_unconfirmed_pattern, bw);

        }
    }
}
