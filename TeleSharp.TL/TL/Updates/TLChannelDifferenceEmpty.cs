using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Updates
{
    [TLObject(1041346555)]
    public class TLChannelDifferenceEmpty : TLAbsChannelDifference
    {
        public override int Constructor
        {
            get
            {
                return 1041346555;
            }
        }

        public int flags { get; set; }
        public bool final { get; set; }
        public int pts { get; set; }
        public int? timeout { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = final ? (flags | 1) : (flags & ~1);
            flags = timeout != null ? (flags | 2) : (flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            final = (flags & 1) != 0;
            pts = br.ReadInt32();
            if ((flags & 2) != 0)
                timeout = br.ReadInt32();
            else
                timeout = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            bw.Write(pts);
            if ((flags & 2) != 0)
                bw.Write(timeout.Value);

        }
    }
}
