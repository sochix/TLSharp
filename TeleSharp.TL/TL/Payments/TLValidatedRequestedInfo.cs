using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Payments
{
    [TLObject(-784000893)]
    public class TLValidatedRequestedInfo : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -784000893;
            }
        }

        public int flags { get; set; }
        public string id { get; set; }
        public TLVector<TLShippingOption> shipping_options { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = id != null ? (flags | 1) : (flags & ~1);
            flags = shipping_options != null ? (flags | 2) : (flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            if ((flags & 1) != 0)
                id = StringUtil.Deserialize(br);
            else
                id = null;

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
            if ((flags & 1) != 0)
                StringUtil.Serialize(id, bw);
            if ((flags & 2) != 0)
                ObjectUtils.SerializeObject(shipping_options, bw);

        }
    }
}
