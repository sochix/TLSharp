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

        public TLAbsInputDocument id { get; set; }
        public bool unfave { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = (TLAbsInputDocument)ObjectUtils.DeserializeObject(br);
            unfave = BoolUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(id, bw);
            BoolUtil.Serialize(unfave, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
