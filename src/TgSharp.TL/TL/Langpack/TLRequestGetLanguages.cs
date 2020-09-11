using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL.Langpack
{
    [TLObject(1120311183)]
    public class TLRequestGetLanguages : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1120311183;
            }
        }

        public string LangPack { get; set; }
        public TLVector<TLLangPackLanguage> Response { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            LangPack = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(LangPack, bw);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLVector<TLLangPackLanguage>)ObjectUtils.DeserializeVector<TLLangPackLanguage>(br);
        }
    }
}
