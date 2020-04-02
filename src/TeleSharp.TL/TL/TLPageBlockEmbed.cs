using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-840826671)]
    public class TLPageBlockEmbed : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return -840826671;
            }
        }

        public int Flags { get; set; }
        public bool FullWidth { get; set; }
        public bool AllowScrolling { get; set; }
        public string Url { get; set; }
        public string Html { get; set; }
        public long? PosterPhotoId { get; set; }
        public int W { get; set; }
        public int H { get; set; }
        public TLAbsRichText Caption { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = FullWidth ? (Flags | 1) : (Flags & ~1);
            Flags = AllowScrolling ? (Flags | 8) : (Flags & ~8);
            Flags = Url != null ? (Flags | 2) : (Flags & ~2);
            Flags = Html != null ? (Flags | 4) : (Flags & ~4);
            Flags = PosterPhotoId != null ? (Flags | 16) : (Flags & ~16);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            FullWidth = (Flags & 1) != 0;
            AllowScrolling = (Flags & 8) != 0;
            if ((Flags & 2) != 0)
                Url = StringUtil.Deserialize(br);
            else
                Url = null;

            if ((Flags & 4) != 0)
                Html = StringUtil.Deserialize(br);
            else
                Html = null;

            if ((Flags & 16) != 0)
                PosterPhotoId = br.ReadInt64();
            else
                PosterPhotoId = null;

            W = br.ReadInt32();
            H = br.ReadInt32();
            Caption = (TLAbsRichText)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);


            if ((Flags & 2) != 0)
                StringUtil.Serialize(Url, bw);
            if ((Flags & 4) != 0)
                StringUtil.Serialize(Html, bw);
            if ((Flags & 16) != 0)
                bw.Write(PosterPhotoId.Value);
            bw.Write(W);
            bw.Write(H);
            ObjectUtils.SerializeObject(Caption, bw);

        }
    }
}
