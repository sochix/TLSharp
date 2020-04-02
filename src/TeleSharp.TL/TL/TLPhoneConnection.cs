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

        public long Id { get; set; }
        public string Ip { get; set; }
        public string Ipv6 { get; set; }
        public int Port { get; set; }
        public byte[] PeerTag { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = br.ReadInt64();
            Ip = StringUtil.Deserialize(br);
            Ipv6 = StringUtil.Deserialize(br);
            Port = br.ReadInt32();
            PeerTag = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Id);
            StringUtil.Serialize(Ip, bw);
            StringUtil.Serialize(Ipv6, bw);
            bw.Write(Port);
            BytesUtil.Serialize(PeerTag, bw);

        }
    }
}
