using System.IO;
namespace TeleSharp.TL
{
    [TLObject(1187706024)]
    public class TLInputMessagesFilterMyMentionsUnread : TLAbsMessagesFilter
    {
        public override int Constructor
        {
            get
            {
                return 1187706024;
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
