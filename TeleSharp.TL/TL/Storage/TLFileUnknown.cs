using System.IO;

namespace TeleSharp.TL.Storage
{
    [TLObject(-1432995067)]
    public class TLFileUnknown : TLAbsFileType
    {
        public override int Constructor => -1432995067;


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