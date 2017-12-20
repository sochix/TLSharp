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

        public TLVector<TLImportedContact> Imported { get; set; }

        public TLVector<TLPopularContact> PopularInvites { get; set; }

        public TLVector<long> RetryContacts { get; set; }

        public TLVector<TLAbsUser> Users { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Imported = (TLVector<TLImportedContact>)ObjectUtils.DeserializeVector<TLImportedContact>(br);
            PopularInvites = (TLVector<TLPopularContact>)ObjectUtils.DeserializeVector<TLPopularContact>(br);
            RetryContacts = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);
            Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Imported, bw);
            ObjectUtils.SerializeObject(PopularInvites, bw);
            ObjectUtils.SerializeObject(RetryContacts, bw);
            ObjectUtils.SerializeObject(Users, bw);
        }
    }
}
