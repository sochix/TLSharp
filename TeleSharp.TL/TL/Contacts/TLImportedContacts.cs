using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Contacts
{
    [TLObject(-1387117803)]
    public class TLImportedContacts : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1387117803;
            }
        }

        public TLVector<TLImportedContact> imported { get; set; }
        public TLVector<long> retry_contacts { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            imported = (TLVector<TLImportedContact>)ObjectUtils.DeserializeVector<TLImportedContact>(br);
            retry_contacts = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);
            users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

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
