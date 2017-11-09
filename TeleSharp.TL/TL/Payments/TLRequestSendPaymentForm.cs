using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Payments
{
    [TLObject(730364339)]
    public class TLRequestSendPaymentForm : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 730364339;
            }
        }

        public int Flags { get; set; }
        public int MsgId { get; set; }
        public string RequestedInfoId { get; set; }
        public string ShippingOptionId { get; set; }
        public TLAbsInputPaymentCredentials Credentials { get; set; }
        public Payments.TLAbsPaymentResult Response { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = RequestedInfoId != null ? (Flags | 1) : (Flags & ~1);
            Flags = ShippingOptionId != null ? (Flags | 2) : (Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            MsgId = br.ReadInt32();
            if ((Flags & 1) != 0)
                RequestedInfoId = StringUtil.Deserialize(br);
            else
                RequestedInfoId = null;

            if ((Flags & 2) != 0)
                ShippingOptionId = StringUtil.Deserialize(br);
            else
                ShippingOptionId = null;

            Credentials = (TLAbsInputPaymentCredentials)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);
            bw.Write(MsgId);
            if ((Flags & 1) != 0)
                StringUtil.Serialize(RequestedInfoId, bw);
            if ((Flags & 2) != 0)
                StringUtil.Serialize(ShippingOptionId, bw);
            ObjectUtils.SerializeObject(Credentials, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Payments.TLAbsPaymentResult)ObjectUtils.DeserializeObject(br);

        }
    }
}
