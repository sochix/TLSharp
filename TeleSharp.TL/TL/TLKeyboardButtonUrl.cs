using System.IO;

namespace TeleSharp.TL
{
    [TLObject(629866245)]
    public class TLKeyboardButtonUrl : TLAbsKeyboardButton
    {
        public override int Constructor
        {
            get
            {
                return 629866245;
            }
        }

        public string Text { get; set; }

        public string Url { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Text = StringUtil.Deserialize(br);
            Url = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Text, bw);
            StringUtil.Serialize(Url, bw);
        }
    }
}
