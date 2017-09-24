using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1458172132)]
    public class TLInputMessagesFilterPhotoVideo : TLAbsMessagesFilter
    {
        public override int Constructor
        {
            get
            {
                return 1458172132;
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