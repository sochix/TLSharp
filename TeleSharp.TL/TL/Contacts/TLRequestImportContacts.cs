using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Contacts
{
    [TLObject(-634342611)]
    public class TLRequestImportContacts : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -634342611;
            }
        }

        public TLVector<TLInputPhoneContact> contacts { get; set; }
        public bool replace { get; set; }
        public Contacts.TLImportedContacts Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            contacts = (TLVector<TLInputPhoneContact>)ObjectUtils.DeserializeVector<TLInputPhoneContact>(br);
            replace = BoolUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(contacts, bw);
            BoolUtil.Serialize(replace, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Contacts.TLImportedContacts)ObjectUtils.DeserializeObject(br);

        }
    }
}
