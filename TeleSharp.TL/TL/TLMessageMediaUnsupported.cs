using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-1618676578)]
    public class TLMessageMediaUnsupported : TLAbsMessageMedia
    {
        public override int Constructor
        {
            get
            {
                return -1618676578;
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
