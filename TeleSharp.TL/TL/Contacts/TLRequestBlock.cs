using System.IO;

namespace TeleSharp.TL.Contacts
{
    [TLObject(858475004)]
    public class TLRequestBlock : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 858475004;
            }
        }

        public TLAbsInputUser Id { get; set; }

        public bool Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Id, bw);
        }
    }
}
