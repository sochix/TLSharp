using System.IO;

namespace TeleSharp.TL
{
    [TLObject(145955919)]
    public class TLPageBlockCollage : TLAbsPageBlock
    {
        public TLAbsRichText Caption { get; set; }

        public override int Constructor
        {
            get
            {
                return 145955919;
            }
        }

        public TLVector<TLAbsPageBlock> Items { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Items = (TLVector<TLAbsPageBlock>)ObjectUtils.DeserializeVector<TLAbsPageBlock>(br);
            Caption = (TLAbsRichText)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Items, bw);
            ObjectUtils.SerializeObject(Caption, bw);
        }
    }
}
