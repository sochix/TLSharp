using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1678812626)]
    public class TLStickerSetCovered : TLAbsStickerSetCovered
    {
        public override int Constructor
        {
            get
            {
                return 1678812626;
            }
        }

        public TLStickerSet @set { get; set; }
        public TLAbsDocument cover { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            @set = (TLStickerSet)ObjectUtils.DeserializeObject(br);
            cover = (TLAbsDocument)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(@set, bw);
            ObjectUtils.SerializeObject(cover, bw);

        }
    }
}
