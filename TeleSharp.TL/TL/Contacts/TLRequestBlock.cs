using System.IO;

namespace TeleSharp.TL.Contacts
{
    [TLObject(858475004)]
    public class TLRequestBlock : TLMethod
    {
        public override int Constructor => 858475004;

        public TLAbsInputUser id { get; set; }
        public bool Response { get; set; }


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
            Response = BoolUtil.Deserialize(br);
        }
    }
}