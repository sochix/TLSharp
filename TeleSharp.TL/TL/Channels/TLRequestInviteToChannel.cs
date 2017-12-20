using System.IO;

namespace TeleSharp.TL.Channels
{
    [TLObject(429865580)]
    public class TLRequestInviteToChannel : TLMethod
    {
        public TLAbsInputChannel Channel { get; set; }

        public override int Constructor
        {
            get
            {
                return 429865580;
            }
        }

        public TLAbsUpdates Response { get; set; }

        public TLVector<TLAbsInputUser> Users { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            Users = (TLVector<TLAbsInputUser>)ObjectUtils.DeserializeVector<TLAbsInputUser>(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Channel, bw);
            ObjectUtils.SerializeObject(Users, bw);
        }
    }
}
