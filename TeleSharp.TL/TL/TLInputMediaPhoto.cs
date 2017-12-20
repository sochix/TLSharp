using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-2114308294)]
    public class TLInputMediaPhoto : TLAbsInputMedia
    {
        public string Caption { get; set; }

        public override int Constructor
        {
            get
            {
                return -2114308294;
            }
        }

        public int Flags { get; set; }

        public TLAbsInputPhoto Id { get; set; }

        public int? TtlSeconds { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Id = (TLAbsInputPhoto)ObjectUtils.DeserializeObject(br);
            Caption = StringUtil.Deserialize(br);
            if ((Flags & 1) != 0)
                TtlSeconds = br.ReadInt32();
            else
                TtlSeconds = null;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            ObjectUtils.SerializeObject(Id, bw);
            StringUtil.Serialize(Caption, bw);
            if ((Flags & 1) != 0)
                bw.Write(TtlSeconds.Value);
        }
    }
}
