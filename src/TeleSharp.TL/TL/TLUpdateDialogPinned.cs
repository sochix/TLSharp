using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1852826908)]
    public class TLUpdateDialogPinned : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 1852826908;
            }
        }

        public int Flags { get; set; }
        public bool Pinned { get; set; }
        public int? FolderId { get; set; }
        public TLAbsDialogPeer Peer { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Pinned = (Flags & 1) != 0;
            if ((Flags & 2) != 0)
                FolderId = br.ReadInt32();
            else
                FolderId = null;

            Peer = (TLAbsDialogPeer)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            if ((Flags & 2) != 0)
                bw.Write(FolderId.Value);
            ObjectUtils.SerializeObject(Peer, bw);

        }
    }
}
