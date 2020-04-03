using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1468953147)]
    public class TLPageBlockEmbed : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return -1468953147;
            }
        }

        public int Flags { get; set; }
        public bool FullWidth { get; set; }
        public bool AllowScrolling { get; set; }
        public string Url { get; set; }
        public string Html { get; set; }
        public long? PosterPhotoId { get; set; }
        public int? W { get; set; }
        public int? H { get; set; }
        public TLPageCaption Caption { get; set; }


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

            if ((Flags & 32) != 0)
                W = br.ReadInt32();
            else
                W = null;

            if ((Flags & 32) != 0)
                H = br.ReadInt32();
            else
                H = null;

            Caption = (TLPageCaption)ObjectUtils.DeserializeObject(br);

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
            if ((Flags & 32) != 0)
                bw.Write(W.Value);
            if ((Flags & 32) != 0)
                bw.Write(H.Value);
            ObjectUtils.SerializeObject(Caption, bw);

        }
    }
}
