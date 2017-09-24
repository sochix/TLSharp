using System.IO;

namespace TeleSharp.TL
{
    [TLObject(381645902)]
    public class TLSendMessageTypingAction : TLAbsSendMessageAction
    {
        public override int Constructor
        {
            get
            {
                return 381645902;
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