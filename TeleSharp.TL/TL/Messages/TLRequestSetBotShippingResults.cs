using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-436833542)]
    public class TLRequestSetBotShippingResults : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -436833542;
            }
        }

        public int flags { get; set; }
        public long query_id { get; set; }
        public string error { get; set; }
        public TLVector<TLShippingOption> shipping_options { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = error != null ? (flags | 1) : (flags & ~1);
            flags = shipping_options != null ? (flags | 2) : (flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            query_id = br.ReadInt64();
            if ((flags & 1) != 0)
                error = StringUtil.Deserialize(br);
            else
                error = null;

            if ((flags & 2) != 0)
                shipping_options = (TLVector<TLShippingOption>)ObjectUtils.DeserializeVector<TLShippingOption>(br);
            else
                shipping_options = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            bw.Write(query_id);
            if ((flags & 1) != 0)
                StringUtil.Serialize(error, bw);
            if ((flags & 2) != 0)
                ObjectUtils.SerializeObject(shipping_options, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
