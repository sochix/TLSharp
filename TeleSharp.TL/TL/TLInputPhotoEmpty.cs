using System.IO;

namespace TeleSharp.TL
{
    [TLObject(483901197)]
    public class TLInputPhotoEmpty : TLAbsInputPhoto
    {
        public override int Constructor
        {
            get
            {
                return 483901197;
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
