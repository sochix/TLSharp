using System.IO;

namespace TeleSharp.TL
{
    [TLObject(834148991)]
    public class TLPageBlockAudio : TLAbsPageBlock
    {
        public long AudioId { get; set; }

        public TLAbsRichText Caption { get; set; }

        public override int Constructor
        {
            get
            {
                return 834148991;
            }
        }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            AudioId = br.ReadInt64();
            Caption = (TLAbsRichText)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(AudioId);
            ObjectUtils.SerializeObject(Caption, bw);
        }
    }
}
