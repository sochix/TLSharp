using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
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

        public bool ordered { get; set; }
        public TLVector<TLAbsRichText> items { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            ordered = BoolUtil.Deserialize(br);
            items = (TLVector<TLAbsRichText>)ObjectUtils.DeserializeVector<TLAbsRichText>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BoolUtil.Serialize(ordered, bw);
            ObjectUtils.SerializeObject(items, bw);

        }
    }
}
