using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Stickers
{
    [TLObject(1322714570)]
    public class TLRequestChangeStickerPosition : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1322714570;
            }
        }

        public TLAbsInputDocument sticker { get; set; }
        public int position { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            sticker = (TLAbsInputDocument)ObjectUtils.DeserializeObject(br);
            position = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(sticker, bw);
            bw.Write(position);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
