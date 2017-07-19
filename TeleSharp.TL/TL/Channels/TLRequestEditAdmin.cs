using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
    [TLObject(-344583728)]
    public class TLRequestEditAdmin : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -344583728;
            }
        }

        public TLAbsInputChannel channel { get; set; }
        public TLAbsInputUser user_id { get; set; }
        public TLAbsChannelParticipantRole role { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            user_id = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            role = (TLAbsChannelParticipantRole)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel, bw);
            ObjectUtils.SerializeObject(user_id, bw);
            ObjectUtils.SerializeObject(role, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
