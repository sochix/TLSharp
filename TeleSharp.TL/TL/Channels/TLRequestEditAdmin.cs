using System.IO;

namespace TeleSharp.TL.Channels
{
    [TLObject(548962836)]
    public class TLRequestEditAdmin : TLMethod
    {
        public TLChannelAdminRights AdminRights { get; set; }

        public TLAbsInputChannel Channel { get; set; }

        public override int Constructor
        {
            get
            {
                return 548962836;
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
            AdminRights = (TLChannelAdminRights)ObjectUtils.DeserializeObject(br);
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
            ObjectUtils.SerializeObject(AdminRights, bw);
        }
    }
}
