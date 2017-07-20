using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Updates
{
    [TLObject(1091431943)]
    public class TLChannelDifferenceTooLong : TLAbsChannelDifference
    {
        public override int Constructor
        {
            get
            {
                return 1091431943;
            }
        }

        public int flags { get; set; }
        public bool final { get; set; }
        public int pts { get; set; }
        public int? timeout { get; set; }
        public int top_message { get; set; }
        public int read_inbox_max_id { get; set; }
        public int read_outbox_max_id { get; set; }
        public int unread_count { get; set; }
        public TLVector<TLAbsMessage> messages { get; set; }
        public TLVector<TLAbsChat> chats { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = final ? (flags | 1) : (flags & ~1);
            flags = timeout != null ? (flags | 2) : (flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            final = (flags & 1) != 0;
            pts = br.ReadInt32();
            if ((flags & 2) != 0)
                timeout = br.ReadInt32();
            else
                timeout = null;

            top_message = br.ReadInt32();
            read_inbox_max_id = br.ReadInt32();
            read_outbox_max_id = br.ReadInt32();
            unread_count = br.ReadInt32();
            messages = (TLVector<TLAbsMessage>)ObjectUtils.DeserializeVector<TLAbsMessage>(br);
            chats = (TLVector<TLAbsChat>)ObjectUtils.DeserializeVector<TLAbsChat>(br);
            users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            bw.Write(pts);
            if ((flags & 2) != 0)
                bw.Write(timeout.Value);
            bw.Write(top_message);
            bw.Write(read_inbox_max_id);
            bw.Write(read_outbox_max_id);
            bw.Write(unread_count);
            ObjectUtils.SerializeObject(messages, bw);
            ObjectUtils.SerializeObject(chats, bw);
            ObjectUtils.SerializeObject(users, bw);

        }
    }
}
