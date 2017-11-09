using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-84416311)]
    public class TLPhoneCallDiscardReasonBusy : TLAbsPhoneCallDiscardReason
    {
        public override int Constructor
        {
            get
            {
                return -84416311;
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
