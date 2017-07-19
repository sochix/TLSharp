using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1844103547)]
    public class TLInputMediaInvoice : TLAbsInputMedia
    {
        public override int Constructor
        {
            get
            {
                return -1844103547;
            }
        }

        public int flags { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public TLInputWebDocument photo { get; set; }
        public TLInvoice invoice { get; set; }
        public byte[] payload { get; set; }
        public string provider { get; set; }
        public string start_param { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = photo != null ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            title = StringUtil.Deserialize(br);
            description = StringUtil.Deserialize(br);
            if ((flags & 1) != 0)
                photo = (TLInputWebDocument)ObjectUtils.DeserializeObject(br);
            else
                photo = null;

            invoice = (TLInvoice)ObjectUtils.DeserializeObject(br);
            payload = BytesUtil.Deserialize(br);
            provider = StringUtil.Deserialize(br);
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
            ObjectUtils.SerializeObject(invoice, bw);
            BytesUtil.Serialize(payload, bw);
            StringUtil.Serialize(provider, bw);
            StringUtil.Serialize(start_param, bw);

        }
    }
}
