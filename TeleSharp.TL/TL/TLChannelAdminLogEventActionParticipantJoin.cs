using System.IO;
namespace TeleSharp.TL
{
    [TLObject(405815507)]
    public class TLChannelAdminLogEventActionParticipantJoin : TLAbsChannelAdminLogEventAction
    {
        public override int Constructor
        {
            get
            {
                return 405815507;
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
