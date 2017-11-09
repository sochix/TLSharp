using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
    [TLObject(-1425815847)]
    public class TLSentCodeTypeFlashCall : TLAbsSentCodeType
    {
        public override int Constructor
        {
            get
            {
                return -1425815847;
            }
        }

        public string Pattern { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Pattern = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Pattern, bw);

        }
    }
}
