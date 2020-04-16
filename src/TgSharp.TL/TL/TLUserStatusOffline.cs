using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(9203775)]
    public class TLUserStatusOffline : TLAbsUserStatus
    {
        public override int Constructor
        {
            get
            {
                return 9203775;
            }
        }

        public int WasOnline { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            WasOnline = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(WasOnline);
        }
    }
}
