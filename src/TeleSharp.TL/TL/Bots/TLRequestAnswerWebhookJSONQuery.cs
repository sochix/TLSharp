using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
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

        public long QueryId { get; set; }
        public TLDataJSON Data { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            QueryId = br.ReadInt64();
            Data = (TLDataJSON)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(QueryId);
            ObjectUtils.SerializeObject(Data, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
