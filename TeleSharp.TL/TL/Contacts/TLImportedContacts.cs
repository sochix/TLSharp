using System.IO;

namespace TeleSharp.TL.Contacts
{
    [TLObject(-1387117803)]
    public class TLImportedContacts : TLObject
    {
        public override int Constructor => -1387117803;

        public TLVector<TLImportedContact> imported { get; set; }
        public TLVector<long> retry_contacts { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            imported = ObjectUtils.DeserializeVector<TLImportedContact>(br);
            retry_contacts = ObjectUtils.DeserializeVector<long>(br);
            users = ObjectUtils.DeserializeVector<TLAbsUser>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(imported, bw);
            ObjectUtils.SerializeObject(retry_contacts, bw);
            ObjectUtils.SerializeObject(users, bw);
        }
    }
}