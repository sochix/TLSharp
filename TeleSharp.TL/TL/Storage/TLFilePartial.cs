using System.IO;

namespace TeleSharp.TL.Storage
{
    [TLObject(1086091090)]
    public class TLFilePartial : TLAbsFileType
    {
        public override int Constructor => 1086091090;


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