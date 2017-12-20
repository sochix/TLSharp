using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1938967520)]
    public class TLMessageEntityPre : TLAbsMessageEntity
    {
        public override int Constructor
        {
            get
            {
                return 1938967520;
            }
        }

        public string Language { get; set; }

        public int Length { get; set; }

        public int Offset { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Offset = br.ReadInt32();
            Length = br.ReadInt32();
            Language = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Offset);
            bw.Write(Length);
            StringUtil.Serialize(Language, bw);
        }
    }
}
