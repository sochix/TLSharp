using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-523384512)]
    public class TLUpdateBotShippingQuery : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -523384512;
            }
        }

        public long query_id { get; set; }
        public int user_id { get; set; }
        public byte[] payload { get; set; }
        public TLPostAddress shipping_address { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            query_id = br.ReadInt64();
            user_id = br.ReadInt32();
            payload = BytesUtil.Deserialize(br);
            shipping_address = (TLPostAddress)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(query_id);
            bw.Write(user_id);
            BytesUtil.Serialize(payload, bw);
            ObjectUtils.SerializeObject(shipping_address, bw);

        }
    }
}
