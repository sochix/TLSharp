using System.IO;

namespace TeleSharp.TL.Help
{
    [TLObject(-236044656)]
    public class TLTermsOfService : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -236044656;
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