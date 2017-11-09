using System.IO;
namespace TeleSharp.TL
{
    [TLObject(1887741886)]
    public class TLUpdateContactsReset : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 1887741886;
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
