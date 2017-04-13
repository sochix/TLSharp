using System.IO;

namespace TeleSharp.TL.Help
{
    [TLObject(-1000708810)]
    public class TLNoAppUpdate : TLAbsAppUpdate
    {
        public override int Constructor => -1000708810;


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