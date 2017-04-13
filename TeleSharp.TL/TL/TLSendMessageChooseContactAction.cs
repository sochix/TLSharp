using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1653390447)]
    public class TLSendMessageChooseContactAction : TLAbsSendMessageAction
    {
        public override int Constructor => 1653390447;


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