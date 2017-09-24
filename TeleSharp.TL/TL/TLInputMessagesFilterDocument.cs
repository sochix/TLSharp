using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1629621880)]
    public class TLInputMessagesFilterDocument : TLAbsMessagesFilter
    {
        public override int Constructor
        {
            get
            {
                return -1629621880;
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