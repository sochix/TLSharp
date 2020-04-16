using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(1577484359)]
    public class TLPageListOrderedItemText : TLAbsPageListOrderedItem
    {
        public override int Constructor
        {
            get
            {
                return 1577484359;
            }
        }

        public string Num { get; set; }
        public TLAbsRichText Text { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Num = StringUtil.Deserialize(br);
            Text = (TLAbsRichText)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Num, bw);
            ObjectUtils.SerializeObject(Text, bw);
        }
    }
}
