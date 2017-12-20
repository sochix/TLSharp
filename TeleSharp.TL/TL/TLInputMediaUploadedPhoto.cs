using System.IO;

namespace TeleSharp.TL
{
    [TLObject(792191537)]
    public class TLInputMediaUploadedPhoto : TLAbsInputMedia
    {
        public string Caption { get; set; }

        public override int Constructor
        {
            get
            {
                return 792191537;
            }
        }

        public TLAbsInputFile File { get; set; }

        public int Flags { get; set; }

        public TLVector<TLAbsInputDocument> Stickers { get; set; }

        public int? TtlSeconds { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            File = (TLAbsInputFile)ObjectUtils.DeserializeObject(br);
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
            StringUtil.Serialize(Caption, bw);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(Stickers, bw);
            if ((Flags & 2) != 0)
                bw.Write(TtlSeconds.Value);
        }
    }
}
