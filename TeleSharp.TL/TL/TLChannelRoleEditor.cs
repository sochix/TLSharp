using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-2113143156)]
    public class TLChannelRoleEditor : TLAbsChannelParticipantRole
    {
        public override int Constructor
        {
            get
            {
                return -2113143156;
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