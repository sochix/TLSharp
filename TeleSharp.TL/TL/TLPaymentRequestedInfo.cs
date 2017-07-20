using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1868808300)]
    public class TLPaymentRequestedInfo : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1868808300;
            }
        }

        public int flags { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public TLPostAddress shipping_address { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = name != null ? (flags | 1) : (flags & ~1);
            flags = phone != null ? (flags | 2) : (flags & ~2);
            flags = email != null ? (flags | 4) : (flags & ~4);
            flags = shipping_address != null ? (flags | 8) : (flags & ~8);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            if ((flags & 1) != 0)
                name = StringUtil.Deserialize(br);
            else
                name = null;

            if ((flags & 2) != 0)
                phone = StringUtil.Deserialize(br);
            else
                phone = null;

            if ((flags & 4) != 0)
                email = StringUtil.Deserialize(br);
            else
                email = null;

            if ((flags & 8) != 0)
                shipping_address = (TLPostAddress)ObjectUtils.DeserializeObject(br);
            else
                shipping_address = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            if ((flags & 1) != 0)
                StringUtil.Serialize(name, bw);
            if ((flags & 2) != 0)
                StringUtil.Serialize(phone, bw);
            if ((flags & 4) != 0)
                StringUtil.Serialize(email, bw);
            if ((flags & 8) != 0)
                ObjectUtils.SerializeObject(shipping_address, bw);

        }
    }
}
