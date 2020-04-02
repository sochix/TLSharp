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

        public TLAbsInputDocument Sticker { get; set; }
        public int Position { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Sticker = (TLAbsInputDocument)ObjectUtils.DeserializeObject(br);
            Position = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Sticker, bw);
            bw.Write(Position);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
