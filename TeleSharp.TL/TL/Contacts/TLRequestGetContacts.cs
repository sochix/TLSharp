using System.IO;

namespace TeleSharp.TL.Contacts
{
    [TLObject(-1071414113)]
    public class TLRequestGetContacts : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1071414113;
            }
        }

        public int Hash { get; set; }

        public Contacts.TLAbsContacts Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Hash = br.ReadInt32();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Contacts.TLAbsContacts)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Hash);
        }
    }
}
