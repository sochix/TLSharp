using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(-1263546448)]
    public class TLUpdatePeerLocated : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -1263546448;
            }
        }

        public TLVector<TLPeerLocated> Peers { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Peers = (TLVector<TLPeerLocated>)ObjectUtils.DeserializeVector<TLPeerLocated>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Peers, bw);
        }
    }
}
