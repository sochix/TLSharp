using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1358283666)]
    public class TLInputMessagesFilterVoice : TLAbsMessagesFilter
    {
        public override int Constructor
        {
            get
            {
                return 1358283666;
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
