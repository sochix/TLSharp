using System.IO;

namespace TeleSharp.TL.Contacts
{
    [TLObject(471043349)]
    public class TLBlocked : TLAbsBlocked
    {
        public override int Constructor => 471043349;

        public TLVector<TLContactBlocked> blocked { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            blocked = ObjectUtils.DeserializeVector<TLContactBlocked>(br);
            users = ObjectUtils.DeserializeVector<TLAbsUser>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(blocked, bw);
            ObjectUtils.SerializeObject(users, bw);
        }
    }
}