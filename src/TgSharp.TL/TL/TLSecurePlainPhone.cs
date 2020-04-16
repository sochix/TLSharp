using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(2103482845)]
    public class TLSecurePlainPhone : TLAbsSecurePlainData
    {
        public override int Constructor
        {
            get
            {
                return 2103482845;
            }
        }

        public string Phone { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Phone = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Phone, bw);
        }
    }
}
