using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
    [TLObject(-787622117)]
    public class TLRequestDeleteUserHistory : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -787622117;
            }
        }

        public TLAbsInputChannel Channel { get; set; }
        public TLAbsInputUser UserId { get; set; }
        public Messages.TLAffectedHistory Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            UserId = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Channel, bw);
            ObjectUtils.SerializeObject(UserId, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAffectedHistory)ObjectUtils.DeserializeObject(br);

        }
    }
}
