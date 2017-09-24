using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-402498398)]
    public class TLSavedGifsNotModified : TLAbsSavedGifs
    {
        public override int Constructor
        {
            get
            {
                return -402498398;
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