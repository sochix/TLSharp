using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1474492012)]
    public class TLInputMessagesFilterEmpty : TLAbsMessagesFilter
    {
        public override int Constructor
        {
            get
            {
                return 1474492012;
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