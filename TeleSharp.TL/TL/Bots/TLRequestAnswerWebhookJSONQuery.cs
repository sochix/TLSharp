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

        public long query_id { get; set; }
        public TLDataJSON data { get; set; }
        public bool Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            query_id = br.ReadInt64();
            data = (TLDataJSON)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(query_id);
            ObjectUtils.SerializeObject(data, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }
    }
}