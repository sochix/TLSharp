using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL.Upload
{
    [TLObject(-290921362)]
    public class TLCdnFileReuploadNeeded : TLAbsCdnFile
    {
        public override int Constructor
        {
            get
            {
                return -290921362;
            }
        }

        public byte[] RequestToken { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            RequestToken = BytesUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(RequestToken, bw);
        }
    }
}
