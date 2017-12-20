using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1791935732)]
    public class TLUpdateUserPhoto : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -1791935732;
            }
        }

        public int Date { get; set; }

        public TLAbsUserProfilePhoto Photo { get; set; }

        public bool Previous { get; set; }

        public int UserId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            UserId = br.ReadInt32();
            Date = br.ReadInt32();
            Photo = (TLAbsUserProfilePhoto)ObjectUtils.DeserializeObject(br);
            Previous = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(UserId);
            bw.Write(Date);
            ObjectUtils.SerializeObject(Photo, bw);
            BoolUtil.Serialize(Previous, bw);
        }
    }
}
