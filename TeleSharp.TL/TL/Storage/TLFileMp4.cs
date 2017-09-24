using System.IO;

namespace TeleSharp.TL.Storage
{
    [TLObject(-1278304028)]
    public class TLFileMp4 : TLAbsFileType
    {
        public override int Constructor
        {
            get
            {
                return -1278304028;
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