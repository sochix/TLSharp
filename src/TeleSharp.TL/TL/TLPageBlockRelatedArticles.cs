using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(370236054)]
    public class TLPageBlockRelatedArticles : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return 370236054;
            }
        }

        public TLAbsRichText Title { get; set; }
        public TLVector<TLPageRelatedArticle> Articles { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Title = (TLAbsRichText)ObjectUtils.DeserializeObject(br);
            Articles = (TLVector<TLPageRelatedArticle>)ObjectUtils.DeserializeVector<TLPageRelatedArticle>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Title, bw);
            ObjectUtils.SerializeObject(Articles, bw);

        }
    }
}
