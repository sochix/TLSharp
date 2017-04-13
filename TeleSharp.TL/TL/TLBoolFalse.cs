using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1132882121)]
    public class TLBoolFalse : TLAbsBool
    {
        public override int Constructor => -1132882121;


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