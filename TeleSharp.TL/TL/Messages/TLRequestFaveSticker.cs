using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-1174420133)]
    public class TLRequestFaveSticker : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1174420133;
            }
        }

        public TLAbsInputDocument Id { get; set; }

        public bool Response { get; set; }

        public bool Unfave { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = (TLAbsInputDocument)ObjectUtils.DeserializeObject(br);
            Unfave = BoolUtil.Deserialize(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Id, bw);
            BoolUtil.Serialize(Unfave, bw);
        }
    }
}
