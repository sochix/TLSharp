using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-820669733)]
    public class TLRequestReportSpam : TLMethod
    {
        public override int Constructor => -820669733;

        public TLAbsInputPeer peer { get; set; }
        public bool Response { get; set; }


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
            Response = BoolUtil.Deserialize(br);
        }
    }
}