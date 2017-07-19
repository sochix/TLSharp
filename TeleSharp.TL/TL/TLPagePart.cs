using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1913754556)]
    public class TLPagePart : TLAbsPage
    {
        public override int Constructor
        {
            get
            {
                return -1913754556;
            }
        }

        public TLVector<TLAbsPageBlock> blocks { get; set; }
        public TLVector<TLAbsPhoto> photos { get; set; }
        public TLVector<TLAbsDocument> videos { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            blocks = (TLVector<TLAbsPageBlock>)ObjectUtils.DeserializeVector<TLAbsPageBlock>(br);
            photos = (TLVector<TLAbsPhoto>)ObjectUtils.DeserializeVector<TLAbsPhoto>(br);
            videos = (TLVector<TLAbsDocument>)ObjectUtils.DeserializeVector<TLAbsDocument>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(blocks, bw);
            ObjectUtils.SerializeObject(photos, bw);
            ObjectUtils.SerializeObject(videos, bw);

        }
    }
}
