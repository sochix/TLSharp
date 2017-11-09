using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-1299865402)]
    public class TLChannelRoleEmpty : TLAbsChannelParticipantRole
    {
        public override int Constructor
        {
            get
            {
                return -1299865402;
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
