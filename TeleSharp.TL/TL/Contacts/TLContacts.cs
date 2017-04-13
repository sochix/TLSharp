using System.IO;

namespace TeleSharp.TL.Contacts
{
    [TLObject(1871416498)]
    public class TLContacts : TLAbsContacts
    {
        public override int Constructor => 1871416498;

        public TLVector<TLContact> contacts { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            contacts = ObjectUtils.DeserializeVector<TLContact>(br);
            users = ObjectUtils.DeserializeVector<TLAbsUser>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(contacts, bw);
            ObjectUtils.SerializeObject(users, bw);
        }
    }
}