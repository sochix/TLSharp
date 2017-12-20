using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1329878739)]
    public class TLPageBlockPullquote : TLAbsPageBlock
    {
        public TLAbsRichText Caption { get; set; }

        public override int Constructor
        {
            get
            {
                return 1329878739;
            }
        }

        public TLAbsRichText Text { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Text = (TLAbsRichText)ObjectUtils.DeserializeObject(br);
            Caption = (TLAbsRichText)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Text, bw);
            ObjectUtils.SerializeObject(Caption, bw);
        }
    }
}
