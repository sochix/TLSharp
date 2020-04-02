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

        public int Flags { get; set; }
        public bool Mask { get; set; }
        public string Alt { get; set; }
        public TLAbsInputStickerSet Stickerset { get; set; }
        public TLMaskCoords MaskCoords { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Mask ? (Flags | 2) : (Flags & ~2);
            Flags = MaskCoords != null ? (Flags | 1) : (Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Mask = (Flags & 2) != 0;
            Alt = StringUtil.Deserialize(br);
            Stickerset = (TLAbsInputStickerSet)ObjectUtils.DeserializeObject(br);
            if ((Flags & 1) != 0)
                MaskCoords = (TLMaskCoords)ObjectUtils.DeserializeObject(br);
            else
                MaskCoords = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);

            StringUtil.Serialize(Alt, bw);
            ObjectUtils.SerializeObject(Stickerset, bw);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(MaskCoords, bw);

        }
    }
}
