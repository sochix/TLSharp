using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Langpack
{
    [TLObject(-269862909)]
    public class TLRequestGetStrings : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -269862909;
            }
        }

        public string LangPack { get; set; }
        public string LangCode { get; set; }
        public TLVector<string> Keys { get; set; }
        public TLVector<TLAbsLangPackString> Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            LangPack = StringUtil.Deserialize(br);
            LangCode = StringUtil.Deserialize(br);
            Keys = (TLVector<string>)ObjectUtils.DeserializeVector<string>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(LangPack, bw);
            StringUtil.Serialize(LangCode, bw);
            ObjectUtils.SerializeObject(Keys, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLVector<TLAbsLangPackString>)ObjectUtils.DeserializeVector<TLAbsLangPackString>(br);

        }
    }
}
