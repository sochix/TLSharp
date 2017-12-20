using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1950782688)]
    public class TLTextPlain : TLAbsRichText
    {
        public override int Constructor
        {
            get
            {
                return 1950782688;
            }
        }

        public string Text { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Text = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Text, bw);
        }
    }
}
