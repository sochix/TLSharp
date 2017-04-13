using System.IO;
using TeleSharp.TL.Messages;

namespace TeleSharp.TL.Channels
{
    [TLObject(-787622117)]
    public class TLRequestDeleteUserHistory : TLMethod
    {
        public override int Constructor => -787622117;

        public TLAbsInputChannel channel { get; set; }
        public TLAbsInputUser user_id { get; set; }
        public TLAffectedHistory Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsInputChannel) ObjectUtils.DeserializeObject(br);
            user_id = (TLAbsInputUser) ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel, bw);
            ObjectUtils.SerializeObject(user_id, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAffectedHistory) ObjectUtils.DeserializeObject(br);
        }
    }
}