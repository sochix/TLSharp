using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(856380452)]
    public class TLUpdateReadChannelInbox : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 856380452;
            }
        }

        public int Flags { get; set; }
        public int? FolderId { get; set; }
        public int ChannelId { get; set; }
        public int MaxId { get; set; }
        public int StillUnreadCount { get; set; }
        public int Pts { get; set; }


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

            ChannelId = br.ReadInt32();
            MaxId = br.ReadInt32();
            StillUnreadCount = br.ReadInt32();
            Pts = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            if ((Flags & 1) != 0)
                bw.Write(FolderId.Value);
            bw.Write(ChannelId);
            bw.Write(MaxId);
            bw.Write(StillUnreadCount);
            bw.Write(Pts);

        }
    }
}
