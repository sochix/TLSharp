using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-484690728)]
    public class TLChannelAdminLogEventActionParticipantInvite : TLAbsChannelAdminLogEventAction
    {
        public override int Constructor
        {
            get
            {
                return -484690728;
            }
        }

        public TLAbsChannelParticipant participant { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            participant = (TLAbsChannelParticipant)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(participant, bw);

        }
    }
}
