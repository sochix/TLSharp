using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1328445861)]
    public class TLChannelParticipantsBots : TLAbsChannelParticipantsFilter
    {
        public override int Constructor => -1328445861;


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
        }
    }
}