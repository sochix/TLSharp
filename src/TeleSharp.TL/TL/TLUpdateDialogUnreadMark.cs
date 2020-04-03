using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-513517117)]
    public class TLUpdateDialogUnreadMark : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -513517117;
            }
        }

        public int Flags { get; set; }
        public bool Unread { get; set; }
        public TLAbsDialogPeer Peer { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Unread = (Flags & 1) != 0;
            Peer = (TLAbsDialogPeer)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            ObjectUtils.SerializeObject(Peer, bw);

        }
    }
}
