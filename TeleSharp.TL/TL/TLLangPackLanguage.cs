using System.IO;
namespace TeleSharp.TL
{
    [TLObject(292985073)]
    public class TLLangPackLanguage : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 292985073;
            }
        }

        public string name { get; set; }
        public string native_name { get; set; }
        public string lang_code { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            name = StringUtil.Deserialize(br);
            native_name = StringUtil.Deserialize(br);
            lang_code = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(name, bw);
            StringUtil.Serialize(native_name, bw);
            StringUtil.Serialize(lang_code, bw);

        }
    }
}
