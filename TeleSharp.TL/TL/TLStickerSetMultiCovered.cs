using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(872932635)]
    public class TLStickerSetMultiCovered : TLAbsStickerSetCovered
    {
        public override int Constructor
        {
            get
            {
                return 872932635;
            }
        }

        public TLStickerSet @set { get; set; }
        public TLVector<TLAbsDocument> covers { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            @set = (TLStickerSet)ObjectUtils.DeserializeObject(br);
            covers = (TLVector<TLAbsDocument>)ObjectUtils.DeserializeVector<TLAbsDocument>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(@set, bw);
            ObjectUtils.SerializeObject(covers, bw);

        }
    }
}
