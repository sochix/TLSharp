using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1010285434)]
    public class TLChannelParticipantsKicked : TLAbsChannelParticipantsFilter
    {
        public override int Constructor => 1010285434;


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