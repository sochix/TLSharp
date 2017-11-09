using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-451831443)]
    public class TLUpdateFavedStickers : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -451831443;
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
