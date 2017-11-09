using System.IO;
namespace TeleSharp.TL.Storage
{
    [TLObject(172975040)]
    public class TLFilePng : TLAbsFileType
    {
        public override int Constructor
        {
            get
            {
                return 172975040;
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
