using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-59151553)]
    public class TLKeyboardButtonRequestGeoLocation : TLAbsKeyboardButton
    {
        public override int Constructor => -59151553;

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