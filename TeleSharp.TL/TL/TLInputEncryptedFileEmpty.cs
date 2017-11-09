using System.IO;
namespace TeleSharp.TL
{
    [TLObject(406307684)]
    public class TLInputEncryptedFileEmpty : TLAbsInputEncryptedFile
    {
        public override int Constructor
        {
            get
            {
                return 406307684;
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
