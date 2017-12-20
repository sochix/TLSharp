using System.IO;

namespace TeleSharp.TL.Phone
{
    [TLObject(-326966976)]
    public class TLPhoneCall : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -326966976;
            }
        }

        public TLAbsPhoneCall PhoneCall { get; set; }

        public TLVector<TLAbsUser> Users { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            PhoneCall = (TLAbsPhoneCall)ObjectUtils.DeserializeObject(br);
            Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(PhoneCall, bw);
            ObjectUtils.SerializeObject(Users, bw);
        }
    }
}
