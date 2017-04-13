using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1615153660)]
    public class TLMessageActionHistoryClear : TLAbsMessageAction
    {
        public override int Constructor => -1615153660;


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