using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(-906486552)]
    public class TLRequestSetPrivacy : TLMethod
    {
        public override int Constructor => -906486552;

        public TLAbsInputPrivacyKey key { get; set; }
        public TLVector<TLAbsInputPrivacyRule> rules { get; set; }
        public TLPrivacyRules Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            key = (TLAbsInputPrivacyKey) ObjectUtils.DeserializeObject(br);
            rules = ObjectUtils.DeserializeVector<TLAbsInputPrivacyRule>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(key, bw);
            ObjectUtils.SerializeObject(rules, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLPrivacyRules) ObjectUtils.DeserializeObject(br);
        }
    }
}