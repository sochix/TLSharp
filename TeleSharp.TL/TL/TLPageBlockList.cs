using System.IO;

namespace TeleSharp.TL
{
    [TLObject(978896884)]
    public class TLPageBlockList : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return 978896884;
            }
        }

        public TLVector<TLAbsRichText> Items { get; set; }

        public bool Ordered { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Ordered = BoolUtil.Deserialize(br);
            Items = (TLVector<TLAbsRichText>)ObjectUtils.DeserializeVector<TLAbsRichText>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BoolUtil.Serialize(Ordered, bw);
            ObjectUtils.SerializeObject(Items, bw);
        }
    }
}
