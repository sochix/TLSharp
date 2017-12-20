using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1038967584)]
    public class TLMessageMediaEmpty : TLAbsMessageMedia
    {
        public override int Constructor
        {
            get
            {
                return 1038967584;
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
