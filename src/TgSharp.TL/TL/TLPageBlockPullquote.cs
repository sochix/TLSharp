using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(1329878739)]
    public class TLPageBlockPullquote : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return 1329878739;
            }
        }

        public TLAbsRichText Text { get; set; }
        public TLAbsRichText Caption { get; set; }

        public void ComputeFlags()
        {
            // do nothing
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
