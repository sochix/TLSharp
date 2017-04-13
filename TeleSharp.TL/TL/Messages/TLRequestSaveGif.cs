using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(846868683)]
    public class TLRequestSaveGif : TLMethod
    {
        public override int Constructor => 846868683;

        public TLAbsInputDocument id { get; set; }
        public bool unsave { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = (TLAbsInputDocument) ObjectUtils.DeserializeObject(br);
            unsave = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(id, bw);
            BoolUtil.Serialize(unsave, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }
    }
}