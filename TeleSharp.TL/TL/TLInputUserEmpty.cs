using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-1182234929)]
    public class TLInputUserEmpty : TLAbsInputUser
    {
        public override int Constructor
        {
            get
            {
                return -1182234929;
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
