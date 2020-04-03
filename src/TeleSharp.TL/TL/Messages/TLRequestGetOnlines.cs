using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(1848369232)]
    public class TLRequestGetOnlines : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1848369232;
            }
        }

        public TLAbsInputPeer Peer { get; set; }
        public TLChatOnlines Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Peer, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLChatOnlines)ObjectUtils.DeserializeObject(br);

        }
    }
}
