using System.IO;

namespace TeleSharp.TL.Channels
{
    [TLObject(-791039645)]
    public class TLChannelParticipant : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -791039645;
            }
        }

        public TLAbsChannelParticipant Participant { get; set; }

        public TLVector<TLAbsUser> Users { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Participant = (TLAbsChannelParticipant)ObjectUtils.DeserializeObject(br);
            Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Participant, bw);
            ObjectUtils.SerializeObject(Users, bw);
        }
    }
}
