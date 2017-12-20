using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-840826671)]
    public class TLPageBlockEmbed : TLAbsPageBlock
    {
        public bool AllowScrolling { get; set; }

        public TLAbsRichText Caption { get; set; }

        public override int Constructor
        {
            get
            {
                return -840826671;
            }
        }

        public int Flags { get; set; }

        public bool FullWidth { get; set; }

        public int H { get; set; }

        public string Html { get; set; }

        public long? PosterPhotoId { get; set; }

        public string Url { get; set; }

        public int W { get; set; }

        public void ComputeFlags()
        {
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
