using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Help
{
    [TLObject(1378703997)]
    public class TLRequestGetAppUpdate : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1378703997;
            }
        }

        public string Source { get; set; }
        public Help.TLAbsAppUpdate Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Source = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Source, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Help.TLAbsAppUpdate)ObjectUtils.DeserializeObject(br);

        }
    }
}
