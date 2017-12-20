using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1777752804)]
    public class TLInputMessagesFilterPhotos : TLAbsMessagesFilter
    {
        public override int Constructor
        {
            get
            {
                return -1777752804;
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
