using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1661770481)]
    public class TLInputMediaUploadedPhoto : TLAbsInputMedia
    {
        public override int Constructor => 1661770481;

        public int flags { get; set; }
        public TLAbsInputFile file { get; set; }
        public string caption { get; set; }
        public TLVector<TLAbsInputDocument> stickers { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = stickers != null ? flags | 1 : flags & ~1;
        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            file = (TLAbsInputFile) ObjectUtils.DeserializeObject(br);
            caption = StringUtil.Deserialize(br);
            if ((flags & 1) != 0)
                stickers = ObjectUtils.DeserializeVector<TLAbsInputDocument>(br);
            else
                stickers = null;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            ObjectUtils.SerializeObject(file, bw);
            StringUtil.Serialize(caption, bw);
            if ((flags & 1) != 0)
                ObjectUtils.SerializeObject(stickers, bw);
        }
    }
}