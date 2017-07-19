using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(913498268)]
    public class TLRequestGetPeerSettings : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 913498268;
            }
        }

        public TLAbsInputPeer peer { get; set; }
        public TLPeerSettings Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLPeerSettings)ObjectUtils.DeserializeObject(br);

        }
    }
}
