using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1799538451)]
    public class TLMessageActionPinMessage : TLAbsMessageAction
    {
        public override int Constructor => -1799538451;


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