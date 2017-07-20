using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(98092748)]
    public class TLDcOption : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 98092748;
            }
        }

        public int flags { get; set; }
        public bool ipv6 { get; set; }
        public bool media_only { get; set; }
        public bool tcpo_only { get; set; }
        public bool cdn { get; set; }
        public int id { get; set; }
        public string ip_address { get; set; }
        public int port { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = ipv6 ? (flags | 1) : (flags & ~1);
            flags = media_only ? (flags | 2) : (flags & ~2);
            flags = tcpo_only ? (flags | 4) : (flags & ~4);
            flags = cdn ? (flags | 8) : (flags & ~8);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            ipv6 = (flags & 1) != 0;
            media_only = (flags & 2) != 0;
            tcpo_only = (flags & 4) != 0;
            cdn = (flags & 8) != 0;
            id = br.ReadInt32();
            ip_address = StringUtil.Deserialize(br);
            port = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);




            bw.Write(id);
            StringUtil.Serialize(ip_address, bw);
            bw.Write(port);

        }
    }
}
