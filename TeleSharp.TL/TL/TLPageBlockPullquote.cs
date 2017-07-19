using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
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

        public TLAbsRichText text { get; set; }
        public TLAbsRichText caption { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            text = (TLAbsRichText)ObjectUtils.DeserializeObject(br);
            caption = (TLAbsRichText)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(text, bw);
            ObjectUtils.SerializeObject(caption, bw);

        }
    }
}
