using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1730311882)]
    public class TLPageListOrderedItemBlocks : TLAbsPageListOrderedItem
    {
        public override int Constructor
        {
            get
            {
                return -1730311882;
            }
        }

        public string Num { get; set; }
        public TLVector<TLAbsPageBlock> Blocks { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Num = StringUtil.Deserialize(br);
            Blocks = (TLVector<TLAbsPageBlock>)ObjectUtils.DeserializeVector<TLAbsPageBlock>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Num, bw);
            ObjectUtils.SerializeObject(Blocks, bw);

        }
    }
}
