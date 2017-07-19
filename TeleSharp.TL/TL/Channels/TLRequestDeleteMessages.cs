using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
    [TLObject(-2067661490)]
    public class TLRequestDeleteMessages : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -2067661490;
            }
        }

        public TLAbsInputChannel channel { get; set; }
        public TLVector<int> id { get; set; }
        public Messages.TLAffectedMessages Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            id = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel, bw);
            ObjectUtils.SerializeObject(id, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAffectedMessages)ObjectUtils.DeserializeObject(br);

        }
    }
}
