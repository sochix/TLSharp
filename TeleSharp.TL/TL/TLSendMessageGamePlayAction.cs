using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-580219064)]
    public class TLSendMessageGamePlayAction : TLAbsSendMessageAction
    {
        public override int Constructor => -580219064;


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