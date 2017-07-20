using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1662637586)]
    public class TLDocumentAttributeSticker : TLAbsDocumentAttribute
    {
        public override int Constructor
        {
            get
            {
                return 1662637586;
            }
        }

        public int flags { get; set; }
        public bool mask { get; set; }
        public string alt { get; set; }
        public TLAbsInputStickerSet stickerset { get; set; }
        public TLMaskCoords mask_coords { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = mask ? (flags | 2) : (flags & ~2);
            flags = mask_coords != null ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            mask = (flags & 2) != 0;
            alt = StringUtil.Deserialize(br);
            stickerset = (TLAbsInputStickerSet)ObjectUtils.DeserializeObject(br);
            if ((flags & 1) != 0)
                mask_coords = (TLMaskCoords)ObjectUtils.DeserializeObject(br);
            else
                mask_coords = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            StringUtil.Serialize(alt, bw);
            ObjectUtils.SerializeObject(stickerset, bw);
            if ((flags & 1) != 0)
                ObjectUtils.SerializeObject(mask_coords, bw);

        }
    }
}
