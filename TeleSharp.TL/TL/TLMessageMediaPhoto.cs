using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1256047857)]
    public class TLMessageMediaPhoto : TLAbsMessageMedia
    {
        public string Caption { get; set; }

        public override int Constructor
        {
            get
            {
                return -1256047857;
            }
        }

        public int Flags { get; set; }

        public TLAbsPhoto Photo { get; set; }

        public int? TtlSeconds { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            if ((Flags & 1) != 0)
                Photo = (TLAbsPhoto)ObjectUtils.DeserializeObject(br);
            else
                Photo = null;

            if ((Flags & 2) != 0)
                Caption = StringUtil.Deserialize(br);
            else
                Caption = null;

            if ((Flags & 4) != 0)
                TtlSeconds = br.ReadInt32();
            else
                TtlSeconds = null;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(Photo, bw);
            if ((Flags & 2) != 0)
                StringUtil.Serialize(Caption, bw);
            if ((Flags & 4) != 0)
                bw.Write(TtlSeconds.Value);
        }
    }
}
