using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Phone
{
    [TLObject(1003664544)]
    public class TLRequestAcceptCall : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1003664544;
            }
        }

        public TLInputPhoneCall peer { get; set; }
        public byte[] g_b { get; set; }
        public TLPhoneCallProtocol protocol { get; set; }
        public Phone.TLPhoneCall Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLInputPhoneCall)ObjectUtils.DeserializeObject(br);
            g_b = BytesUtil.Deserialize(br);
            protocol = (TLPhoneCallProtocol)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
            BytesUtil.Serialize(g_b, bw);
            ObjectUtils.SerializeObject(protocol, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Phone.TLPhoneCall)ObjectUtils.DeserializeObject(br);

        }
    }
}
