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

        public int g { get; set; }
        public byte[] p { get; set; }
        public int version { get; set; }
        public byte[] random { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            g = br.ReadInt32();
            p = BytesUtil.Deserialize(br);
            version = br.ReadInt32();
            random = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(g);
            BytesUtil.Serialize(p, bw);
            bw.Write(version);
            BytesUtil.Serialize(random, bw);

        }
    }
}
