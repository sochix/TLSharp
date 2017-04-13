using System.IO;

namespace TeleSharp.TL.Storage
{
    [TLObject(-891180321)]
    public class TLFileGif : TLAbsFileType
    {
        public override int Constructor => -891180321;


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