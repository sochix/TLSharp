using System.IO;

namespace TeleSharp.TL
{
    [TLObject(320652927)]
    public class TLInputPrivacyValueAllowUsers : TLAbsInputPrivacyRule
    {
        public override int Constructor => 320652927;

        public TLVector<TLAbsInputUser> users { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            users = ObjectUtils.DeserializeVector<TLAbsInputUser>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(users, bw);
        }
    }
}