using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-352032773)]
    public class TLUpdateChannelTooLong : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -352032773;
            }
        }

        public int flags { get; set; }
        public int channel_id { get; set; }
        public int? pts { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = pts != null ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            channel_id = br.ReadInt32();
            if ((flags & 1) != 0)
                pts = br.ReadInt32();
            else
                pts = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            bw.Write(channel_id);
            if ((flags & 1) != 0)
                bw.Write(pts.Value);

        }
    }
}
