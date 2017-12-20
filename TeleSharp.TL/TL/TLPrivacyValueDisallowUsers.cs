using System.IO;

namespace TeleSharp.TL
{
    [TLObject(209668535)]
    public class TLPrivacyValueDisallowUsers : TLAbsPrivacyRule
    {
        public override int Constructor
        {
            get
            {
                return 209668535;
            }
        }

        public TLVector<int> Users { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Users = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Users, bw);
        }
    }
}
