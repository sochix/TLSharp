using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-99664734)]
    public class TLUpdatePinnedDialogs : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -99664734;
            }
        }

        public int Flags { get; set; }
        public int? FolderId { get; set; }
        public TLVector<TLAbsDialogPeer> Order { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            if ((Flags & 2) != 0)
                FolderId = br.ReadInt32();
            else
                FolderId = null;

            if ((Flags & 1) != 0)
                Order = (TLVector<TLAbsDialogPeer>)ObjectUtils.DeserializeVector<TLAbsDialogPeer>(br);
            else
                Order = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            if ((Flags & 2) != 0)
                bw.Write(FolderId.Value);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(Order, bw);

        }
    }
}
