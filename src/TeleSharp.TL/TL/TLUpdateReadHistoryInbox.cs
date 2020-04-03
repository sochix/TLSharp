using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1667805217)]
    public class TLUpdateReadHistoryInbox : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -1667805217;
            }
        }

        public int Flags { get; set; }
        public int? FolderId { get; set; }
        public TLAbsPeer Peer { get; set; }
        public int MaxId { get; set; }
        public int StillUnreadCount { get; set; }
        public int Pts { get; set; }
        public int PtsCount { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            if ((Flags & 1) != 0)
                FolderId = br.ReadInt32();
            else
                FolderId = null;

            Peer = (TLAbsPeer)ObjectUtils.DeserializeObject(br);
            MaxId = br.ReadInt32();
            StillUnreadCount = br.ReadInt32();
            Pts = br.ReadInt32();
            PtsCount = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            if ((Flags & 1) != 0)
                bw.Write(FolderId.Value);
            ObjectUtils.SerializeObject(Peer, bw);
            bw.Write(MaxId);
            bw.Write(StillUnreadCount);
            bw.Write(Pts);
            bw.Write(PtsCount);

        }
    }
}
