using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Payments
{
    [TLObject(1997180532)]
    public class TLRequestValidateRequestedInfo : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1997180532;
            }
        }

        public int flags { get; set; }
        public bool save { get; set; }
        public int msg_id { get; set; }
        public TLPaymentRequestedInfo info { get; set; }
        public Payments.TLValidatedRequestedInfo Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = save ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            save = (flags & 1) != 0;
            msg_id = br.ReadInt32();
            info = (TLPaymentRequestedInfo)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            bw.Write(msg_id);
            ObjectUtils.SerializeObject(info, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Payments.TLValidatedRequestedInfo)ObjectUtils.DeserializeObject(br);

        }
    }
}
