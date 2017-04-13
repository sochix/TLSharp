using System.IO;

namespace TeleSharp.TL.Auth
{
    [TLObject(1948046307)]
    public class TLCodeTypeCall : TLAbsCodeType
    {
        public override int Constructor => 1948046307;


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