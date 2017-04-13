using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-44119819)]
    public class TLSendMessageCancelAction : TLAbsSendMessageAction
    {
        public override int Constructor => -44119819;


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