using System.IO;

namespace TeleSharp.TL
{
    [TLObject(423314455)]
    public class TLInputNotifyUsers : TLAbsInputNotifyPeer
    {
        public override int Constructor
        {
            get
            {
                return 423314455;
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