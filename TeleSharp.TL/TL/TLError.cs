using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-994444869)]
    public class TLError : TLObject
    {
        public override int Constructor => -994444869;

        public int code { get; set; }
        public string text { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            code = br.ReadInt32();
            text = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(code);
            StringUtil.Serialize(text, bw);
        }
    }
}