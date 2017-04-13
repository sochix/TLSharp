using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(913498268)]
    public class TLRequestGetPeerSettings : TLMethod
    {
        public override int Constructor => 913498268;

        public TLAbsInputPeer peer { get; set; }
        public TLPeerSettings Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLAbsInputPeer) ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLPeerSettings) ObjectUtils.DeserializeObject(br);
        }
    }
}