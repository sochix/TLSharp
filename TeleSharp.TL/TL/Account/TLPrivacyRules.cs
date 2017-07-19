using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(1430961007)]
    public class TLPrivacyRules : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1430961007;
            }
        }

        public TLVector<TLAbsPrivacyRule> rules { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            rules = (TLVector<TLAbsPrivacyRule>)ObjectUtils.DeserializeVector<TLAbsPrivacyRule>(br);
            users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(rules, bw);
            ObjectUtils.SerializeObject(users, bw);

        }
    }
}
