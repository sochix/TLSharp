using System.IO;

namespace TeleSharp.TL.Contacts
{
    [TLObject(986597452)]
    public class TLLink : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 986597452;
            }
        }

        public TLAbsContactLink ForeignLink { get; set; }

        public TLAbsContactLink MyLink { get; set; }

        public TLAbsUser User { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            MyLink = (TLAbsContactLink)ObjectUtils.DeserializeObject(br);
            ForeignLink = (TLAbsContactLink)ObjectUtils.DeserializeObject(br);
            User = (TLAbsUser)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(MyLink, bw);
            ObjectUtils.SerializeObject(ForeignLink, bw);
            ObjectUtils.SerializeObject(User, bw);
        }
    }
}
