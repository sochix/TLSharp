using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(690781161)]
    public class TLPageBlockEmbedPost : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return 690781161;
            }
        }

        public string url { get; set; }
        public long webpage_id { get; set; }
        public long author_photo_id { get; set; }
        public string author { get; set; }
        public int date { get; set; }
        public TLVector<TLAbsPageBlock> blocks { get; set; }
        public TLAbsRichText caption { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            url = StringUtil.Deserialize(br);
            webpage_id = br.ReadInt64();
            author_photo_id = br.ReadInt64();
            author = StringUtil.Deserialize(br);
            date = br.ReadInt32();
            blocks = (TLVector<TLAbsPageBlock>)ObjectUtils.DeserializeVector<TLAbsPageBlock>(br);
            caption = (TLAbsRichText)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(url, bw);
            bw.Write(webpage_id);
            bw.Write(author_photo_id);
            StringUtil.Serialize(author, bw);
            bw.Write(date);
            ObjectUtils.SerializeObject(blocks, bw);
            ObjectUtils.SerializeObject(caption, bw);

        }
    }
}
