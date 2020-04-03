using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(1398240377)]
    public class TLRequestGetNotifyExceptions : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1398240377;
            }
        }

        public int Flags { get; set; }
        public bool CompareSound { get; set; }
        public TLAbsInputNotifyPeer Peer { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            CompareSound = (Flags & 2) != 0;
            if ((Flags & 1) != 0)
                Peer = (TLAbsInputNotifyPeer)ObjectUtils.DeserializeObject(br);
            else
                Peer = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(Peer, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
