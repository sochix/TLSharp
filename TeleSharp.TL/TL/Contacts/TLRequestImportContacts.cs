using System.IO;

namespace TeleSharp.TL.Contacts
{
    [TLObject(-634342611)]
    public class TLRequestImportContacts : TLMethod
    {
        public override int Constructor => -634342611;

        public TLVector<TLInputPhoneContact> contacts { get; set; }
        public bool replace { get; set; }
        public TLImportedContacts Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            contacts = ObjectUtils.DeserializeVector<TLInputPhoneContact>(br);
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
            Response = (TLImportedContacts) ObjectUtils.DeserializeObject(br);
        }
    }
}