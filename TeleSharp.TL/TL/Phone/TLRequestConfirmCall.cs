using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Phone
{
    [TLObject(788404002)]
    public class TLRequestConfirmCall : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 788404002;
            }
        }

        public TLInputPhoneCall peer { get; set; }
        public byte[] g_a { get; set; }
        public long key_fingerprint { get; set; }
        public TLPhoneCallProtocol protocol { get; set; }
        public Phone.TLPhoneCall Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLInputPhoneCall)ObjectUtils.DeserializeObject(br);
            g_a = BytesUtil.Deserialize(br);
            key_fingerprint = br.ReadInt64();
            protocol = (TLPhoneCallProtocol)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
            BytesUtil.Serialize(g_a, bw);
            bw.Write(key_fingerprint);
            ObjectUtils.SerializeObject(protocol, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Phone.TLPhoneCall)ObjectUtils.DeserializeObject(br);

        }
    }
}
