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

        public string text { get; set; }
        public string url { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            text = StringUtil.Deserialize(br);
            url = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(text, bw);
            StringUtil.Serialize(url, bw);

        }
    }
}
