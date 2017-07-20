using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1564789301)]
    public class TLPhoneCallProtocol : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1564789301;
            }
        }

        public int flags { get; set; }
        public bool udp_p2p { get; set; }
        public bool udp_reflector { get; set; }
        public int min_layer { get; set; }
        public int max_layer { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = udp_p2p ? (flags | 1) : (flags & ~1);
            flags = udp_reflector ? (flags | 2) : (flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            udp_p2p = (flags & 1) != 0;
            udp_reflector = (flags & 2) != 0;
            min_layer = br.ReadInt32();
            max_layer = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);


            bw.Write(min_layer);
            bw.Write(max_layer);

        }
    }
}
