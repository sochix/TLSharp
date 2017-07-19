using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
    [TLObject(-871347913)]
    public class TLRequestReadHistory : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -871347913;
            }
        }

        public TLAbsInputChannel channel { get; set; }
        public int max_id { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            max_id = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel, bw);
            bw.Write(max_id);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
