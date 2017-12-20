using System.IO;

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

        public byte[] GB { get; set; }

        public TLInputPhoneCall Peer { get; set; }

        public TLPhoneCallProtocol Protocol { get; set; }

        public Phone.TLPhoneCall Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Peer = (TLInputPhoneCall)ObjectUtils.DeserializeObject(br);
            GB = BytesUtil.Deserialize(br);
            Protocol = (TLPhoneCallProtocol)ObjectUtils.DeserializeObject(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Phone.TLPhoneCall)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Peer, bw);
            BytesUtil.Serialize(GB, bw);
            ObjectUtils.SerializeObject(Protocol, bw);
        }
    }
}
