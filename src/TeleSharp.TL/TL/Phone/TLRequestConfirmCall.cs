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

        public TLInputPhoneCall Peer { get; set; }
        public byte[] GA { get; set; }
        public long KeyFingerprint { get; set; }
        public TLPhoneCallProtocol Protocol { get; set; }
        public Phone.TLPhoneCall Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Peer = (TLInputPhoneCall)ObjectUtils.DeserializeObject(br);
            GA = BytesUtil.Deserialize(br);
            KeyFingerprint = br.ReadInt64();
            Protocol = (TLPhoneCallProtocol)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Peer, bw);
            BytesUtil.Serialize(GA, bw);
            bw.Write(KeyFingerprint);
            ObjectUtils.SerializeObject(Protocol, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Phone.TLPhoneCall)ObjectUtils.DeserializeObject(br);

        }
    }
}
