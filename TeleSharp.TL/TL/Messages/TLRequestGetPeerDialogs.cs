using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(764901049)]
    public class TLRequestGetPeerDialogs : TLMethod
    {
        public override int Constructor => 764901049;

        public TLVector<TLAbsInputPeer> peers { get; set; }
        public TLPeerDialogs Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            peers = ObjectUtils.DeserializeVector<TLAbsInputPeer>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peers, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLPeerDialogs) ObjectUtils.DeserializeObject(br);
        }
    }
}