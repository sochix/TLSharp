using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-462373635)]
    public class TLRequestGetPeerDialogs : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -462373635;
            }
        }

        public TLVector<TLAbsInputDialogPeer> Peers { get; set; }
        public Messages.TLPeerDialogs Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Peers = (TLVector<TLAbsInputDialogPeer>)ObjectUtils.DeserializeVector<TLAbsInputDialogPeer>(br);

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
