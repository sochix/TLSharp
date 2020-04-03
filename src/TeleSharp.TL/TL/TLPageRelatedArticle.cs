using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1282352120)]
    public class TLPageRelatedArticle : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1282352120;
            }
        }

        public int Flags { get; set; }
        public string Url { get; set; }
        public long WebpageId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long? PhotoId { get; set; }
        public string Author { get; set; }
        public int? PublishedDate { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Url = StringUtil.Deserialize(br);
            WebpageId = br.ReadInt64();
            if ((Flags & 1) != 0)
                Title = StringUtil.Deserialize(br);
            else
                Title = null;

            if ((Flags & 2) != 0)
                Description = StringUtil.Deserialize(br);
            else
                Description = null;

            if ((Flags & 4) != 0)
                PhotoId = br.ReadInt64();
            else
                PhotoId = null;

            if ((Flags & 8) != 0)
                Author = StringUtil.Deserialize(br);
            else
                Author = null;

            if ((Flags & 16) != 0)
                PublishedDate = br.ReadInt32();
            else
                PublishedDate = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            StringUtil.Serialize(Url, bw);
            bw.Write(WebpageId);
            if ((Flags & 1) != 0)
                StringUtil.Serialize(Title, bw);
            if ((Flags & 2) != 0)
                StringUtil.Serialize(Description, bw);
            if ((Flags & 4) != 0)
                bw.Write(PhotoId.Value);
            if ((Flags & 8) != 0)
                StringUtil.Serialize(Author, bw);
            if ((Flags & 16) != 0)
                bw.Write(PublishedDate.Value);

        }
    }
}
