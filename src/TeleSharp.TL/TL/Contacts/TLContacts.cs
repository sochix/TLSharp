using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
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

        public TLVector<TLContact> Contacts { get; set; }
        public int SavedCount { get; set; }
        public TLVector<TLAbsUser> Users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Contacts = (TLVector<TLContact>)ObjectUtils.DeserializeVector<TLContact>(br);
            SavedCount = br.ReadInt32();
            Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Contacts, bw);
            bw.Write(SavedCount);
            ObjectUtils.SerializeObject(Users, bw);

        }
    }
}
