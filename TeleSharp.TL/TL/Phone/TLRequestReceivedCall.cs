using System.IO;

namespace TeleSharp.TL.Phone
{
    [TLObject(399855457)]
    public class TLRequestReceivedCall : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 399855457;
            }
        }

        public TLInputPhoneCall Peer { get; set; }

        public bool Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Peer = (TLInputPhoneCall)ObjectUtils.DeserializeObject(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Peer, bw);
        }
    }
}
