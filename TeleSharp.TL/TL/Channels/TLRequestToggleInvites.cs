using System.IO;

namespace TeleSharp.TL.Channels
{
    [TLObject(1231065863)]
    public class TLRequestToggleInvites : TLMethod
    {
        public override int Constructor => 1231065863;

        public TLAbsInputChannel channel { get; set; }
        public bool enabled { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsInputChannel) ObjectUtils.DeserializeObject(br);
            enabled = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel, bw);
            BoolUtil.Serialize(enabled, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates) ObjectUtils.DeserializeObject(br);
        }
    }
}