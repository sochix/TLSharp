using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(-1374118561)]
    public class TLRequestReportPeer : TLMethod
    {
        public override int Constructor => -1374118561;

        public TLAbsInputPeer peer { get; set; }
        public TLAbsReportReason reason { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLAbsInputPeer) ObjectUtils.DeserializeObject(br);
            reason = (TLAbsReportReason) ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
            ObjectUtils.SerializeObject(reason, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }
    }
}