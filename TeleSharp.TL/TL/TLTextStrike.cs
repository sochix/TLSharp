using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1678197867)]
    public class TLTextStrike : TLAbsRichText
    {
        public override int Constructor
        {
            get
            {
                return -1678197867;
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