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

        public int FromVersion { get; set; }

        public string LangCode { get; set; }

        public TLVector<TLAbsLangPackString> Strings { get; set; }

        public int Version { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            LangCode = StringUtil.Deserialize(br);
            FromVersion = br.ReadInt32();
            Version = br.ReadInt32();
            Strings = (TLVector<TLAbsLangPackString>)ObjectUtils.DeserializeVector<TLAbsLangPackString>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(LangCode, bw);
            bw.Write(FromVersion);
            bw.Write(Version);
            ObjectUtils.SerializeObject(Strings, bw);
        }
    }
}
