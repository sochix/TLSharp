using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-1318425559)]
    public class TLKeyboardButtonRequestPhone : TLAbsKeyboardButton
    {
        public override int Constructor
        {
            get
            {
                return -1318425559;
            }
        }

        public string text { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            text = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(text, bw);

        }
    }
}
