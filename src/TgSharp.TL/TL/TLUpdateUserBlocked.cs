using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(-2131957734)]
    public class TLUpdateUserBlocked : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -2131957734;
            }
        }

        public int UserId { get; set; }
        public bool Blocked { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            UserId = br.ReadInt32();
            Blocked = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(UserId);
            BoolUtil.Serialize(Blocked, bw);
        }
    }
}
