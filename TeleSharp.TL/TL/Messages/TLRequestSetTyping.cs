using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-1551737264)]
    public class TLRequestSetTyping : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1551737264;
            }
        }

        public TLAbsInputPeer peer { get; set; }
        public TLAbsSendMessageAction action { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            action = (TLAbsSendMessageAction)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
            ObjectUtils.SerializeObject(action, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
