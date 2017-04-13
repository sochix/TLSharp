using System.IO;

namespace TeleSharp.TL.Channels
{
    [TLObject(-791039645)]
    public class TLChannelParticipant : TLObject
    {
        public override int Constructor => -791039645;

        public TLAbsChannelParticipant participant { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            participant = (TLAbsChannelParticipant) ObjectUtils.DeserializeObject(br);
            users = ObjectUtils.DeserializeVector<TLAbsUser>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(participant, bw);
            ObjectUtils.SerializeObject(users, bw);
        }
    }
}