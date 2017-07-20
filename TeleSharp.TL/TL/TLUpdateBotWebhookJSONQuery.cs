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

        public long query_id { get; set; }
        public TLDataJSON data { get; set; }
        public int timeout { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            query_id = br.ReadInt64();
            data = (TLDataJSON)ObjectUtils.DeserializeObject(br);
            timeout = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(query_id);
            ObjectUtils.SerializeObject(data, bw);
            bw.Write(timeout);

        }
    }
}
