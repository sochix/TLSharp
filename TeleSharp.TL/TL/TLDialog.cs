using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1728035348)]
    public class TLDialog : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1728035348;
            }
        }

        public int flags { get; set; }
        public bool pinned { get; set; }
        public TLAbsPeer peer { get; set; }
        public int top_message { get; set; }
        public int read_inbox_max_id { get; set; }
        public int read_outbox_max_id { get; set; }
        public int unread_count { get; set; }
        public TLAbsPeerNotifySettings notify_settings { get; set; }
        public int? pts { get; set; }
        public TLAbsDraftMessage draft { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = pinned ? (flags | 4) : (flags & ~4);
            flags = pts != null ? (flags | 1) : (flags & ~1);
            flags = draft != null ? (flags | 2) : (flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            pinned = (flags & 4) != 0;
            peer = (TLAbsPeer)ObjectUtils.DeserializeObject(br);
            top_message = br.ReadInt32();
            read_inbox_max_id = br.ReadInt32();
            read_outbox_max_id = br.ReadInt32();
            unread_count = br.ReadInt32();
            notify_settings = (TLAbsPeerNotifySettings)ObjectUtils.DeserializeObject(br);
            if ((flags & 1) != 0)
                pts = br.ReadInt32();
            else
                pts = null;

            if ((flags & 2) != 0)
                draft = (TLAbsDraftMessage)ObjectUtils.DeserializeObject(br);
            else
                draft = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            ObjectUtils.SerializeObject(peer, bw);
            bw.Write(top_message);
            bw.Write(read_inbox_max_id);
            bw.Write(read_outbox_max_id);
            bw.Write(unread_count);
            ObjectUtils.SerializeObject(notify_settings, bw);
            if ((flags & 1) != 0)
                bw.Write(pts.Value);
            if ((flags & 2) != 0)
                ObjectUtils.SerializeObject(draft, bw);

        }
    }
}
