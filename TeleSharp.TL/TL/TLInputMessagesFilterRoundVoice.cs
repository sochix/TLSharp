using System.IO;
namespace TeleSharp.TL
{
    [TLObject(2054952868)]
    public class TLInputMessagesFilterRoundVoice : TLAbsMessagesFilter
    {
        public override int Constructor
        {
            get
            {
                return 2054952868;
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
