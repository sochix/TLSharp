using System.IO;

namespace TeleSharp.TL.Contacts
{
    [TLObject(746589157)]
    public class TLRequestImportContacts : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 746589157;
            }
        }

        public TLVector<TLInputPhoneContact> Contacts { get; set; }

        public Contacts.TLImportedContacts Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Contacts = (TLVector<TLInputPhoneContact>)ObjectUtils.DeserializeVector<TLInputPhoneContact>(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Contacts.TLImportedContacts)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Contacts, bw);
        }
    }
}
