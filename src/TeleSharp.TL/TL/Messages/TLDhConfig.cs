using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(740433629)]
    public class TLDhConfig : TLAbsDhConfig
    {
        public override int Constructor
        {
            get
            {
                return 740433629;
            }
        }

        public int G { get; set; }
        public byte[] P { get; set; }
        public int Version { get; set; }
        public byte[] Random { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            G = br.ReadInt32();
            P = BytesUtil.Deserialize(br);
            Version = br.ReadInt32();
            Random = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(G);
            BytesUtil.Serialize(P, bw);
            bw.Write(Version);
            BytesUtil.Serialize(Random, bw);

        }
    }
}
