using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL.Auth
{
    [TLObject(326715557)]
    public class TLPasswordRecovery : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 326715557;
            }
        }

        public string EmailPattern { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            EmailPattern = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(EmailPattern, bw);
        }
    }
}
