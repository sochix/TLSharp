using System.IO;
namespace TeleSharp.TL
{
    [TLObject(1200788123)]
    public class TLMessageActionScreenshotTaken : TLAbsMessageAction
    {
        public override int Constructor
        {
            get
            {
                return 1200788123;
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
