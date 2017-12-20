using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-640214938)]
    public class TLPageBlockVideo : TLAbsPageBlock
    {
        public bool Autoplay { get; set; }

        public TLAbsRichText Caption { get; set; }

        public override int Constructor
        {
            get
            {
                return -640214938;
            }
        }

        public int Flags { get; set; }

        public bool Loop { get; set; }

        public long VideoId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Autoplay = (Flags & 1) != 0;
            Loop = (Flags & 2) != 0;
            VideoId = br.ReadInt64();
            Caption = (TLAbsRichText)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);


            bw.Write(VideoId);
            ObjectUtils.SerializeObject(Caption, bw);
        }
    }
}
