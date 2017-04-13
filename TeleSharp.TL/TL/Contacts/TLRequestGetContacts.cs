using System.IO;

namespace TeleSharp.TL.Contacts
{
    [TLObject(583445000)]
    public class TLRequestGetContacts : TLMethod
    {
        public override int Constructor => 583445000;

        public string hash { get; set; }
        public TLAbsContacts Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            hash = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(hash, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsContacts) ObjectUtils.DeserializeObject(br);
        }
    }
}