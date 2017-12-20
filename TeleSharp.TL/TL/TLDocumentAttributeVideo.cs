using System.IO;

namespace TeleSharp.TL
{
    [TLObject(250621158)]
    public class TLDocumentAttributeVideo : TLAbsDocumentAttribute
    {
        public override int Constructor
        {
            get
            {
                return 250621158;
            }
        }

        public int Duration { get; set; }

        public int Flags { get; set; }

        public int H { get; set; }

        public bool RoundMessage { get; set; }

        public int W { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            RoundMessage = (Flags & 1) != 0;
            Duration = br.ReadInt32();
            W = br.ReadInt32();
            H = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            bw.Write(Duration);
            bw.Write(W);
            bw.Write(H);
        }
    }
}
