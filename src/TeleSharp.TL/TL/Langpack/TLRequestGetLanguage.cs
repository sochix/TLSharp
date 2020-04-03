using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Langpack
{
    [TLObject(1784243458)]
    public class TLRequestGetLanguage : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1784243458;
            }
        }

        public string LangPack { get; set; }
        public string LangCode { get; set; }
        public TLLangPackLanguage Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            LangPack = StringUtil.Deserialize(br);
            LangCode = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(LangPack, bw);
            StringUtil.Serialize(LangCode, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLLangPackLanguage)ObjectUtils.DeserializeObject(br);

        }
    }
}
