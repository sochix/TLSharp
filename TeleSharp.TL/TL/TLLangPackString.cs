using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-892239370)]
    public class TLLangPackString : TLAbsLangPackString
    {
        public override int Constructor
        {
            get
            {
                return -892239370;
            }
        }

        public string key { get; set; }
        public string @value { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            key = StringUtil.Deserialize(br);
            @value = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(key, bw);
            StringUtil.Serialize(@value, bw);

        }
    }
}
