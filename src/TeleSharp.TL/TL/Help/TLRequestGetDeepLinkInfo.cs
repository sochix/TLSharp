using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Help
{
    [TLObject(1072547679)]
    public class TLRequestGetDeepLinkInfo : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1072547679;
            }
        }

        public string Path { get; set; }
        public Help.TLAbsDeepLinkInfo Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Path = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Path, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Help.TLAbsDeepLinkInfo)ObjectUtils.DeserializeObject(br);

        }
    }
}
