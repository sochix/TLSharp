using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-1040652646)]
    public class TLInputMessagesFilterMyMentions : TLAbsMessagesFilter
    {
        public override int Constructor
        {
            get
            {
                return -1040652646;
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
