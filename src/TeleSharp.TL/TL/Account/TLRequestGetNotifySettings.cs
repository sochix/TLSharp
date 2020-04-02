using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(313765169)]
    public class TLRequestGetNotifySettings : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 313765169;
            }
        }

        public TLAbsInputNotifyPeer Peer { get; set; }
        public TLAbsPeerNotifySettings Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Peer = (TLAbsInputNotifyPeer)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Peer, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsPeerNotifySettings)ObjectUtils.DeserializeObject(br);

        }
    }
}
