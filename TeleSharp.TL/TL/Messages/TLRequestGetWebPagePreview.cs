using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(623001124)]
    public class TLRequestGetWebPagePreview : TLMethod
    {
        public override int Constructor => 623001124;

        public string message { get; set; }
        public TLAbsMessageMedia Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            message = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(message, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsMessageMedia) ObjectUtils.DeserializeObject(br);
        }
    }
}