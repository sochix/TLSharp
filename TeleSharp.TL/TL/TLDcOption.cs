using System.IO;

namespace TeleSharp.TL
{
    [TLObject(98092748)]
    public class TLDcOption : TLObject
    {
        public bool Cdn { get; set; }

        public override int Constructor
        {
            get
            {
                return 98092748;
            }
        }

        public int Flags { get; set; }

        public int Id { get; set; }

        public string IpAddress { get; set; }

        public bool Ipv6 { get; set; }

        public bool MediaOnly { get; set; }

        public int Port { get; set; }

        public bool Static { get; set; }

        public bool TcpoOnly { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Ipv6 = (Flags & 1) != 0;
            MediaOnly = (Flags & 2) != 0;
            TcpoOnly = (Flags & 4) != 0;
            Cdn = (Flags & 8) != 0;
            Static = (Flags & 16) != 0;
            Id = br.ReadInt32();
            IpAddress = StringUtil.Deserialize(br);
            Port = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);





            bw.Write(Id);
            StringUtil.Serialize(IpAddress, bw);
            bw.Write(Port);
        }
    }
}
