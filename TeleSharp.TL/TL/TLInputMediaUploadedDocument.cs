using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-476700163)]
    public class TLInputMediaUploadedDocument : TLAbsInputMedia
    {
        public TLVector<TLAbsDocumentAttribute> Attributes { get; set; }

        public string Caption { get; set; }

        public override int Constructor
        {
            get
            {
                return -476700163;
            }
        }

        public TLAbsInputFile File { get; set; }

        public int Flags { get; set; }

        public string MimeType { get; set; }

        public TLVector<TLAbsInputDocument> Stickers { get; set; }

        public TLAbsInputFile Thumb { get; set; }

        public int? TtlSeconds { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            File = (TLAbsInputFile)ObjectUtils.DeserializeObject(br);
            if ((Flags & 4) != 0)
                Thumb = (TLAbsInputFile)ObjectUtils.DeserializeObject(br);
            else
                Thumb = null;

            MimeType = StringUtil.Deserialize(br);
            Attributes = (TLVector<TLAbsDocumentAttribute>)ObjectUtils.DeserializeVector<TLAbsDocumentAttribute>(br);
            Caption = StringUtil.Deserialize(br);
            if ((Flags & 1) != 0)
                Stickers = (TLVector<TLAbsInputDocument>)ObjectUtils.DeserializeVector<TLAbsInputDocument>(br);
            else
                Stickers = null;

            if ((Flags & 2) != 0)
                TtlSeconds = br.ReadInt32();
            else
                TtlSeconds = null;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            ObjectUtils.SerializeObject(File, bw);
            if ((Flags & 4) != 0)
                ObjectUtils.SerializeObject(Thumb, bw);
            StringUtil.Serialize(MimeType, bw);
            ObjectUtils.SerializeObject(Attributes, bw);
            StringUtil.Serialize(Caption, bw);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(Stickers, bw);
            if ((Flags & 2) != 0)
                bw.Write(TtlSeconds.Value);
        }
    }
}
