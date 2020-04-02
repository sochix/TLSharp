using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(764901049)]
    public class TLRequestGetPeerDialogs : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 764901049;
            }
        }

        public TLVector<TLAbsInputPeer> Peers { get; set; }
        public Messages.TLPeerDialogs Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Peers = (TLVector<TLAbsInputPeer>)ObjectUtils.DeserializeVector<TLAbsInputPeer>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Peers, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLPeerDialogs)ObjectUtils.DeserializeObject(br);

        }
    }
}
