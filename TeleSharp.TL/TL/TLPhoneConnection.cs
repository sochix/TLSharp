using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1655957568)]
    public class TLPhoneConnection : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1655957568;
            }
        }

        public long id { get; set; }
        public string ip { get; set; }
        public string ipv6 { get; set; }
        public int port { get; set; }
        public byte[] peer_tag { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt64();
            ip = StringUtil.Deserialize(br);
            ipv6 = StringUtil.Deserialize(br);
            port = br.ReadInt32();
            peer_tag = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(id);
            StringUtil.Serialize(ip, bw);
            StringUtil.Serialize(ipv6, bw);
            bw.Write(port);
            BytesUtil.Serialize(peer_tag, bw);

        }
    }
}
