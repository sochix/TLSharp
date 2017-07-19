using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-1058912715)]
    public class TLDhConfigNotModified : TLAbsDhConfig
    {
        public override int Constructor
        {
            get
            {
                return -1058912715;
            }
        }

        public byte[] random { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            random = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(random, bw);

        }
    }
}
