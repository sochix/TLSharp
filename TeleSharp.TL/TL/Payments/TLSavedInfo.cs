using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Payments
{
    [TLObject(-74456004)]
    public class TLSavedInfo : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -74456004;
            }
        }

        public int flags { get; set; }
        public bool has_saved_credentials { get; set; }
        public TLPaymentRequestedInfo saved_info { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = has_saved_credentials ? (flags | 2) : (flags & ~2);
            flags = saved_info != null ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            has_saved_credentials = (flags & 2) != 0;
            if ((flags & 1) != 0)
                saved_info = (TLPaymentRequestedInfo)ObjectUtils.DeserializeObject(br);
            else
                saved_info = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            if ((flags & 1) != 0)
                ObjectUtils.SerializeObject(saved_info, bw);

        }
    }
}
