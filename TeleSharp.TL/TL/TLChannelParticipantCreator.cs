using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-471670279)]
    public class TLChannelParticipantCreator : TLAbsChannelParticipant
    {
        public override int Constructor
        {
            get
            {
                return -471670279;
            }
        }

        public int UserId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            UserId = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(UserId);
        }
    }
}
