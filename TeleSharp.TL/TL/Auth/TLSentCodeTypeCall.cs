using System.IO;

namespace TeleSharp.TL.Auth
{
    [TLObject(1398007207)]
    public class TLSentCodeTypeCall : TLAbsSentCodeType
    {
        public override int Constructor
        {
            get
            {
                return 1398007207;
            }
        }

        public int length { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            length = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(length);
        }
    }
}