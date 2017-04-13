using System.IO;

namespace TeleSharp.TL
{
    [TLObject(209668535)]
    public class TLPrivacyValueDisallowUsers : TLAbsPrivacyRule
    {
        public override int Constructor => 209668535;

        public TLVector<int> users { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            users = ObjectUtils.DeserializeVector<int>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(users, bw);
        }
    }
}