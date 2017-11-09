using System.IO;
namespace TeleSharp.TL.Langpack
{
    [TLObject(-1699363442)]
    public class TLRequestGetLangPack : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1699363442;
            }
        }

        public string lang_code { get; set; }
        public TLLangPackDifference Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            lang_code = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(lang_code, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLLangPackDifference)ObjectUtils.DeserializeObject(br);

        }
    }
}
