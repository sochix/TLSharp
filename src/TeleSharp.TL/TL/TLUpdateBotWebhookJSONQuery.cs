using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1684914010)]
    public class TLUpdateBotWebhookJSONQuery : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -1684914010;
            }
        }

        public long QueryId { get; set; }
        public TLDataJSON Data { get; set; }
        public int Timeout { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            QueryId = br.ReadInt64();
            Data = (TLDataJSON)ObjectUtils.DeserializeObject(br);
            Timeout = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(QueryId);
            ObjectUtils.SerializeObject(Data, bw);
            bw.Write(Timeout);

        }
    }
}
