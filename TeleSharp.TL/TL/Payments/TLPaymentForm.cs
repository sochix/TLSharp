using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Payments
{
    [TLObject(1062645411)]
    public class TLPaymentForm : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1062645411;
            }
        }

        public int flags { get; set; }
        public bool can_save_credentials { get; set; }
        public bool password_missing { get; set; }
        public int bot_id { get; set; }
        public TLInvoice invoice { get; set; }
        public int provider_id { get; set; }
        public string url { get; set; }
        public string native_provider { get; set; }
        public TLDataJSON native_params { get; set; }
        public TLPaymentRequestedInfo saved_info { get; set; }
        public TLPaymentSavedCredentialsCard saved_credentials { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = can_save_credentials ? (flags | 4) : (flags & ~4);
            flags = password_missing ? (flags | 8) : (flags & ~8);
            flags = native_provider != null ? (flags | 16) : (flags & ~16);
            flags = native_params != null ? (flags | 16) : (flags & ~16);
            flags = saved_info != null ? (flags | 1) : (flags & ~1);
            flags = saved_credentials != null ? (flags | 2) : (flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            can_save_credentials = (flags & 4) != 0;
            password_missing = (flags & 8) != 0;
            bot_id = br.ReadInt32();
            invoice = (TLInvoice)ObjectUtils.DeserializeObject(br);
            provider_id = br.ReadInt32();
            url = StringUtil.Deserialize(br);
            if ((flags & 16) != 0)
                native_provider = StringUtil.Deserialize(br);
            else
                native_provider = null;

            if ((flags & 16) != 0)
                native_params = (TLDataJSON)ObjectUtils.DeserializeObject(br);
            else
                native_params = null;

            if ((flags & 1) != 0)
                saved_info = (TLPaymentRequestedInfo)ObjectUtils.DeserializeObject(br);
            else
                saved_info = null;

            if ((flags & 2) != 0)
                saved_credentials = (TLPaymentSavedCredentialsCard)ObjectUtils.DeserializeObject(br);
            else
                saved_credentials = null;

            users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);


            bw.Write(bot_id);
            ObjectUtils.SerializeObject(invoice, bw);
            bw.Write(provider_id);
            StringUtil.Serialize(url, bw);
            if ((flags & 16) != 0)
                StringUtil.Serialize(native_provider, bw);
            if ((flags & 16) != 0)
                ObjectUtils.SerializeObject(native_params, bw);
            if ((flags & 1) != 0)
                ObjectUtils.SerializeObject(saved_info, bw);
            if ((flags & 2) != 0)
                ObjectUtils.SerializeObject(saved_credentials, bw);
            ObjectUtils.SerializeObject(users, bw);

        }
    }
}
