using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1780220945)]
    public class TLMessageActionChatDeletePhoto : TLAbsMessageAction
    {
        public override int Constructor => -1780220945;


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