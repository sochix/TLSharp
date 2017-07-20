using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(-2067899501)]
    public class TLRequestUpdateNotifySettings : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -2067899501;
            }
        }

        public TLAbsInputNotifyPeer peer { get; set; }
        public TLInputPeerNotifySettings settings { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLAbsInputNotifyPeer)ObjectUtils.DeserializeObject(br);
            settings = (TLInputPeerNotifySettings)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
            ObjectUtils.SerializeObject(settings, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
