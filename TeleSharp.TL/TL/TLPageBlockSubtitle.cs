using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1879401953)]
    public class TLPageBlockSubtitle : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return -1879401953;
            }
        }

        public TLAbsRichText text { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            text = (TLAbsRichText)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(text, bw);
        }
    }
}