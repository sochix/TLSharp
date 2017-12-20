using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-2134272152)]
    public class TLInputMessagesFilterPhoneCalls : TLAbsMessagesFilter
    {
        public override int Constructor
        {
            get
            {
                return -2134272152;
            }
        }

        public int Flags { get; set; }

        public bool Missed { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Missed = (Flags & 1) != 0;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
        }
    }
}
