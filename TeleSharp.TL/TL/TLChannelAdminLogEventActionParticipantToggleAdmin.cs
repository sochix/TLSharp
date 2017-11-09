using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-714643696)]
    public class TLChannelAdminLogEventActionParticipantToggleAdmin : TLAbsChannelAdminLogEventAction
    {
        public override int Constructor
        {
            get
            {
                return -714643696;
            }
        }

        public TLAbsChannelParticipant prev_participant { get; set; }
        public TLAbsChannelParticipant new_participant { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            prev_participant = (TLAbsChannelParticipant)ObjectUtils.DeserializeObject(br);
            new_participant = (TLAbsChannelParticipant)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(prev_participant, bw);
            ObjectUtils.SerializeObject(new_participant, bw);

        }
    }
}
