using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-1560655744)]
    public class TLKeyboardButton : TLAbsKeyboardButton
    {
        public override int Constructor
        {
            get
            {
                return -1560655744;
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
