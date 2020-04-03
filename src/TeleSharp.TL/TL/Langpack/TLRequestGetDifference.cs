using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Langpack
{
    [TLObject(-845657435)]
    public class TLRequestGetDifference : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -845657435;
            }
        }

        public string LangPack { get; set; }
        public string LangCode { get; set; }
        public int FromVersion { get; set; }
        public TLLangPackDifference Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            LangPack = StringUtil.Deserialize(br);
            LangCode = StringUtil.Deserialize(br);
            FromVersion = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(LangPack, bw);
            StringUtil.Serialize(LangCode, bw);
            bw.Write(FromVersion);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLLangPackDifference)ObjectUtils.DeserializeObject(br);

        }
    }
}
