using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(-623130288)]
    public class TLRequestGetPrivacy : TLMethod
    {
        public override int Constructor => -623130288;

        public TLAbsInputPrivacyKey key { get; set; }
        public TLPrivacyRules Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            key = (TLAbsInputPrivacyKey) ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(key, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLPrivacyRules) ObjectUtils.DeserializeObject(br);
        }
    }
}