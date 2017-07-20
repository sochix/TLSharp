using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Stickers
{
    [TLObject(-2041315650)]
    public class TLRequestAddStickerToSet : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -2041315650;
            }
        }

        public TLAbsInputStickerSet stickerset { get; set; }
        public TLInputStickerSetItem sticker { get; set; }
        public Messages.TLStickerSet Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            stickerset = (TLAbsInputStickerSet)ObjectUtils.DeserializeObject(br);
            sticker = (TLInputStickerSetItem)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(stickerset, bw);
            ObjectUtils.SerializeObject(sticker, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLStickerSet)ObjectUtils.DeserializeObject(br);

        }
    }
}
