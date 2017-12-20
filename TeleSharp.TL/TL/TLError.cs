using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-994444869)]
    public class TLError : TLObject
    {
        public int Code { get; set; }

        public override int Constructor
        {
            get
            {
                return -994444869;
            }
        }

        public string Text { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Code = br.ReadInt32();
            Text = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Code);
            StringUtil.Serialize(Text, bw);
        }
    }
}
