using System.IO;

namespace TeleSharp.TL.Auth
{
    [TLObject(1923290508)]
    public class TLCodeTypeSms : TLAbsCodeType
    {
        public override int Constructor => 1923290508;


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