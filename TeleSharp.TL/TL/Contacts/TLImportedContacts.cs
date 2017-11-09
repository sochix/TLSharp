using System.IO;
namespace TeleSharp.TL.Contacts
{
    [TLObject(2010127419)]
    public class TLImportedContacts : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 2010127419;
            }
        }

        public TLVector<TLImportedContact> imported { get; set; }
        public TLVector<TLPopularContact> popular_invites { get; set; }
        public TLVector<long> retry_contacts { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            imported = (TLVector<TLImportedContact>)ObjectUtils.DeserializeVector<TLImportedContact>(br);
            popular_invites = (TLVector<TLPopularContact>)ObjectUtils.DeserializeVector<TLPopularContact>(br);
            retry_contacts = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);
            users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(imported, bw);
            ObjectUtils.SerializeObject(popular_invites, bw);
            ObjectUtils.SerializeObject(retry_contacts, bw);
            ObjectUtils.SerializeObject(users, bw);

        }
    }
}
