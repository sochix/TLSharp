using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(-1702174239)]
    public class TLPageBlockOrderedList : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return -1702174239;
            }
        }

        public TLVector<TLAbsPageListOrderedItem> Items { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Items = (TLVector<TLAbsPageListOrderedItem>)ObjectUtils.DeserializeVector<TLAbsPageListOrderedItem>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Items, bw);
        }
    }
}
