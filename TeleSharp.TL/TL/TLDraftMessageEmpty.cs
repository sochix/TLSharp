using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-1169445179)]
    public class TLDraftMessageEmpty : TLAbsDraftMessage
    {
        public override int Constructor
        {
            get
            {
                return -1169445179;
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
