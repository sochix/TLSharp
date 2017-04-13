using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(238054714)]
    public class TLRequestReadHistory : TLMethod
    {
        public override int Constructor => 238054714;

        public TLAbsInputPeer peer { get; set; }
        public int max_id { get; set; }
        public TLAffectedMessages Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLAbsInputPeer) ObjectUtils.DeserializeObject(br);
            max_id = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
            bw.Write(max_id);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAffectedMessages) ObjectUtils.DeserializeObject(br);
        }
    }
}