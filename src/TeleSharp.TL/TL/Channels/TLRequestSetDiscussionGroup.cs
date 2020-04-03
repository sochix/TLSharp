using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
    [TLObject(1079520178)]
    public class TLRequestSetDiscussionGroup : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1079520178;
            }
        }

        public TLAbsInputChannel Broadcast { get; set; }
        public TLAbsInputChannel Group { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Broadcast = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            Group = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Broadcast, bw);
            ObjectUtils.SerializeObject(Group, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
