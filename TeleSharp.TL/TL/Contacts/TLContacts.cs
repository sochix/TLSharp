using System.IO;
namespace TeleSharp.TL.Contacts
{
    [TLObject(-353862078)]
    public class TLContacts : TLAbsContacts
    {
        public override int Constructor
        {
            get
            {
                return -353862078;
            }
        }

        public TLVector<TLContact> contacts { get; set; }
        public int saved_count { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            contacts = (TLVector<TLContact>)ObjectUtils.DeserializeVector<TLContact>(br);
            saved_count = br.ReadInt32();
            users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(contacts, bw);
            bw.Write(saved_count);
            ObjectUtils.SerializeObject(users, bw);

        }
    }
}
