using System.IO;

namespace TeleSharp.TL.Storage
{
    [TLObject(1258941372)]
    public class TLFileMov : TLAbsFileType
    {
        public override int Constructor
        {
            get
            {
                return 1258941372;
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