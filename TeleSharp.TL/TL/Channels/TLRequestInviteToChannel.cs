using System.IO;

namespace TeleSharp.TL.Channels
{
    [TLObject(429865580)]
    public class TLRequestInviteToChannel : TLMethod
    {
        public override int Constructor => 429865580;

        public TLAbsInputChannel channel { get; set; }
        public TLVector<TLAbsInputUser> users { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsInputChannel) ObjectUtils.DeserializeObject(br);
            users = ObjectUtils.DeserializeVector<TLAbsInputUser>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel, bw);
            ObjectUtils.SerializeObject(users, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates) ObjectUtils.DeserializeObject(br);
        }
    }
}