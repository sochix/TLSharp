using System.IO;

namespace TeleSharp.TL.Contacts
{
    [TLObject(-1902823612)]
    public class TLRequestDeleteContact : TLMethod
    {
        public override int Constructor => -1902823612;

        public TLAbsInputUser id { get; set; }
        public TLLink Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = (TLAbsInputUser) ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(id, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLLink) ObjectUtils.DeserializeObject(br);
        }
    }
}