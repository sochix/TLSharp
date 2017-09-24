using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-6249322)]
    public class TLInputStickerSetItem : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -6249322;
            }
        }

        public int flags { get; set; }
        public TLAbsInputDocument document { get; set; }
        public string emoji { get; set; }
        public TLMaskCoords mask_coords { get; set; }

        public void ComputeFlags()
        {
            flags = 0;
            flags = mask_coords != null ? (flags | 1) : (flags & ~1);
        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            document = (TLAbsInputDocument)ObjectUtils.DeserializeObject(br);
            emoji = StringUtil.Deserialize(br);
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
            ObjectUtils.SerializeObject(document, bw);
            StringUtil.Serialize(emoji, bw);
            if ((flags & 1) != 0)
                ObjectUtils.SerializeObject(mask_coords, bw);
        }
    }
}