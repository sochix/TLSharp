using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-209337866)]
    public class TLLangPackDifference : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -209337866;
            }
        }

        public string lang_code { get; set; }
        public int from_version { get; set; }
        public int version { get; set; }
        public TLVector<TLAbsLangPackString> strings { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            lang_code = StringUtil.Deserialize(br);
            from_version = br.ReadInt32();
            version = br.ReadInt32();
            strings = (TLVector<TLAbsLangPackString>)ObjectUtils.DeserializeVector<TLAbsLangPackString>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(lang_code, bw);
            bw.Write(from_version);
            bw.Write(version);
            ObjectUtils.SerializeObject(strings, bw);

        }
    }
}
