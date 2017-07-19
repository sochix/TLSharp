using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
    [TLObject(-1814580409)]
    public class TLRequestGetMessages : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1814580409;
            }
        }

        public TLAbsInputChannel channel { get; set; }
        public TLVector<int> id { get; set; }
        public Messages.TLAbsMessages Response { get; set; }


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
            Response = (Messages.TLAbsMessages)ObjectUtils.DeserializeObject(br);

        }
    }
}
