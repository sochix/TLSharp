using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-2074799289)]
    public class TLMessageMediaInvoice : TLAbsMessageMedia
    {
        public override int Constructor
        {
            get
            {
                return -2074799289;
            }
        }

        public int flags { get; set; }
        public bool shipping_address_requested { get; set; }
        public bool test { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public TLWebDocument photo { get; set; }
        public int? receipt_msg_id { get; set; }
        public string currency { get; set; }
        public long total_amount { get; set; }
        public string start_param { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = shipping_address_requested ? (flags | 2) : (flags & ~2);
            flags = test ? (flags | 8) : (flags & ~8);
            flags = photo != null ? (flags | 1) : (flags & ~1);
            flags = receipt_msg_id != null ? (flags | 4) : (flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            shipping_address_requested = (flags & 2) != 0;
            test = (flags & 8) != 0;
            title = StringUtil.Deserialize(br);
            description = StringUtil.Deserialize(br);
            if ((flags & 1) != 0)
                photo = (TLWebDocument)ObjectUtils.DeserializeObject(br);
            else
                photo = null;

            if ((flags & 4) != 0)
                receipt_msg_id = br.ReadInt32();
            else
                receipt_msg_id = null;

            currency = StringUtil.Deserialize(br);
            total_amount = br.ReadInt64();
            start_param = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);


            StringUtil.Serialize(title, bw);
            StringUtil.Serialize(description, bw);
            if ((flags & 1) != 0)
                ObjectUtils.SerializeObject(photo, bw);
            if ((flags & 4) != 0)
                bw.Write(receipt_msg_id.Value);
            StringUtil.Serialize(currency, bw);
            bw.Write(total_amount);
            StringUtil.Serialize(start_param, bw);

        }
    }
}
