using System.IO;

namespace TeleSharp.TL.Contacts
{
    [TLObject(471043349)]
    public class TLBlocked : TLAbsBlocked
    {
        public TLVector<TLContactBlocked> Blocked { get; set; }

        public override int Constructor
        {
            get
            {
                return 471043349;
            }
        }

        public TLVector<TLAbsUser> Users { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Blocked = (TLVector<TLContactBlocked>)ObjectUtils.DeserializeVector<TLContactBlocked>(br);
            Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Blocked, bw);
            ObjectUtils.SerializeObject(Users, bw);
        }
    }
}
