using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(-1955338397)]
    public class TLPrivacyValueDisallowAll : TLAbsPrivacyRule
    {
        public override int Constructor
        {
            get
            {
                return -1955338397;
            }
        }

        // no fields

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            // do nothing
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            // do nothing
        }
    }
}
