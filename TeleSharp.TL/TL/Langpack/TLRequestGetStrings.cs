using System.IO;
namespace TeleSharp.TL.Langpack
{
    [TLObject(773776152)]
    public class TLRequestGetStrings : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 773776152;
            }
        }

        public string lang_code { get; set; }
        public TLVector<string> keys { get; set; }
        public TLVector<TLAbsLangPackString> Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            lang_code = StringUtil.Deserialize(br);
            keys = (TLVector<string>)ObjectUtils.DeserializeVector<string>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(lang_code, bw);
            ObjectUtils.SerializeObject(keys, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLVector<TLAbsLangPackString>)ObjectUtils.DeserializeVector<TLAbsLangPackString>(br);

        }
    }
}
