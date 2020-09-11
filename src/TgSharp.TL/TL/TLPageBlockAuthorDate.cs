using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(-1162877472)]
    public class TLPageBlockAuthorDate : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return -1162877472;
            }
        }

        public TLAbsRichText Author { get; set; }
        public int PublishedDate { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Author = (TLAbsRichText)ObjectUtils.DeserializeObject(br);
            PublishedDate = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Author, bw);
            bw.Write(PublishedDate);
        }
    }
}
