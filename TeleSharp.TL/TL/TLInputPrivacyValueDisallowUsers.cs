using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1877932953)]
    public class TLInputPrivacyValueDisallowUsers : TLAbsInputPrivacyRule
    {
        public override int Constructor => -1877932953;

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