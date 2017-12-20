using System.IO;

namespace TeleSharp.TL.Channels
{
    [TLObject(-1076292147)]
    public class TLRequestEditBanned : TLMethod
    {
        public TLChannelBannedRights BannedRights { get; set; }

        public TLAbsInputChannel Channel { get; set; }

        public override int Constructor
        {
            get
            {
                return -1076292147;
            }
        }

        public TLAbsUpdates Response { get; set; }

        public TLAbsInputUser UserId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            UserId = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            BannedRights = (TLChannelBannedRights)ObjectUtils.DeserializeObject(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Channel, bw);
            ObjectUtils.SerializeObject(UserId, bw);
            ObjectUtils.SerializeObject(BannedRights, bw);
        }
    }
}
