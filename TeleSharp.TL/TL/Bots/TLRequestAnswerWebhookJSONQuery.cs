using System.IO;

namespace TeleSharp.TL.Bots
{
    [TLObject(-434028723)]
    public class TLRequestAnswerWebhookJSONQuery : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -434028723;
            }
        }

        public TLDataJSON Data { get; set; }

        public long QueryId { get; set; }

        public bool Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            QueryId = br.ReadInt64();
            Data = (TLDataJSON)ObjectUtils.DeserializeObject(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(QueryId);
            ObjectUtils.SerializeObject(Data, bw);
        }
    }
}
