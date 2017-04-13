using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(313765169)]
    public class TLRequestGetNotifySettings : TLMethod
    {
        public override int Constructor => 313765169;

        public TLAbsInputNotifyPeer peer { get; set; }
        public TLAbsPeerNotifySettings Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLAbsInputNotifyPeer) ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsPeerNotifySettings) ObjectUtils.DeserializeObject(br);
        }
    }
}