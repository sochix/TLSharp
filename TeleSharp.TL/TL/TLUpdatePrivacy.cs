using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-298113238)]
    public class TLUpdatePrivacy : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -298113238;
            }
        }

        public TLAbsPrivacyKey key { get; set; }
        public TLVector<TLAbsPrivacyRule> rules { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            key = (TLAbsPrivacyKey)ObjectUtils.DeserializeObject(br);
            rules = (TLVector<TLAbsPrivacyRule>)ObjectUtils.DeserializeVector<TLAbsPrivacyRule>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(key, bw);
            ObjectUtils.SerializeObject(rules, bw);
        }
    }
}