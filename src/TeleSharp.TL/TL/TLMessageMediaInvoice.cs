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

        public int Flags { get; set; }
        public bool ShippingAddressRequested { get; set; }
        public bool Test { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TLWebDocument Photo { get; set; }
        public int? ReceiptMsgId { get; set; }
        public string Currency { get; set; }
        public long TotalAmount { get; set; }
        public string StartParam { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = ShippingAddressRequested ? (Flags | 2) : (Flags & ~2);
            Flags = Test ? (Flags | 8) : (Flags & ~8);
            Flags = Photo != null ? (Flags | 1) : (Flags & ~1);
            Flags = ReceiptMsgId != null ? (Flags | 4) : (Flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            ShippingAddressRequested = (Flags & 2) != 0;
            Test = (Flags & 8) != 0;
            Title = StringUtil.Deserialize(br);
            Description = StringUtil.Deserialize(br);
            if ((Flags & 1) != 0)
                Photo = (TLWebDocument)ObjectUtils.DeserializeObject(br);
            else
                Photo = null;

            if ((Flags & 4) != 0)
                ReceiptMsgId = br.ReadInt32();
            else
                ReceiptMsgId = null;

            Currency = StringUtil.Deserialize(br);
            TotalAmount = br.ReadInt64();
            StartParam = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);


            StringUtil.Serialize(Title, bw);
            StringUtil.Serialize(Description, bw);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(Photo, bw);
            if ((Flags & 4) != 0)
                bw.Write(ReceiptMsgId.Value);
            StringUtil.Serialize(Currency, bw);
            bw.Write(TotalAmount);
            StringUtil.Serialize(StartParam, bw);

        }
    }
}
