using System.IO;

namespace TeleSharp.TL
{
    [TLObject(641563686)]
    public class TLPageBlockBlockquote : TLAbsPageBlock
    {
        public TLAbsRichText Caption { get; set; }

        public override int Constructor
        {
            get
            {
                return 641563686;
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
