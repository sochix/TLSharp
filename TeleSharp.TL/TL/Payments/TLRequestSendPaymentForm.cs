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

        public int flags { get; set; }
        public int msg_id { get; set; }
        public string requested_info_id { get; set; }
        public string shipping_option_id { get; set; }
        public TLAbsInputPaymentCredentials credentials { get; set; }
        public Payments.TLAbsPaymentResult Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = requested_info_id != null ? (flags | 1) : (flags & ~1);
            flags = shipping_option_id != null ? (flags | 2) : (flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            msg_id = br.ReadInt32();
            if ((flags & 1) != 0)
                requested_info_id = StringUtil.Deserialize(br);
            else
                requested_info_id = null;

            if ((flags & 2) != 0)
                shipping_option_id = StringUtil.Deserialize(br);
            else
                shipping_option_id = null;

            credentials = (TLAbsInputPaymentCredentials)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            bw.Write(msg_id);
            if ((flags & 1) != 0)
                StringUtil.Serialize(requested_info_id, bw);
            if ((flags & 2) != 0)
                StringUtil.Serialize(shipping_option_id, bw);
            ObjectUtils.SerializeObject(credentials, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Payments.TLAbsPaymentResult)ObjectUtils.DeserializeObject(br);

        }
    }
}
