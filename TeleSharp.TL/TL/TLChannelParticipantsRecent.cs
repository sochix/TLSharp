using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-566281095)]
    public class TLChannelParticipantsRecent : TLAbsChannelParticipantsFilter
    {
        public override int Constructor
        {
            get
            {
                return -566281095;
            }
        }

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
