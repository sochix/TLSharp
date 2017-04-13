using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(1430961007)]
    public class TLPrivacyRules : TLObject
    {
        public override int Constructor => 1430961007;

        public TLVector<TLAbsPrivacyRule> rules { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            rules = ObjectUtils.DeserializeVector<TLAbsPrivacyRule>(br);
            users = ObjectUtils.DeserializeVector<TLAbsUser>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(rules, bw);
            ObjectUtils.SerializeObject(users, bw);
        }
    }
}