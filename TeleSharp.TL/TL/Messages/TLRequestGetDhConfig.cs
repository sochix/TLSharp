using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(651135312)]
    public class TLRequestGetDhConfig : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 651135312;
            }
        }

        public int version { get; set; }
        public int random_length { get; set; }
        public Messages.TLAbsDhConfig Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            version = br.ReadInt32();
            random_length = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(version);
            bw.Write(random_length);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAbsDhConfig)ObjectUtils.DeserializeObject(br);

        }
    }
}
