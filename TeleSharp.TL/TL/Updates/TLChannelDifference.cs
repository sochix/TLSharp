using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Updates
{
    [TLObject(543450958)]
    public class TLChannelDifference : TLAbsChannelDifference
    {
        public override int Constructor
        {
            get
            {
                return 543450958;
            }
        }

        public int flags { get; set; }
        public bool final { get; set; }
        public int pts { get; set; }
        public int? timeout { get; set; }
        public TLVector<TLAbsMessage> new_messages { get; set; }
        public TLVector<TLAbsUpdate> other_updates { get; set; }
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

            new_messages = (TLVector<TLAbsMessage>)ObjectUtils.DeserializeVector<TLAbsMessage>(br);
            other_updates = (TLVector<TLAbsUpdate>)ObjectUtils.DeserializeVector<TLAbsUpdate>(br);
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
            ObjectUtils.SerializeObject(new_messages, bw);
            ObjectUtils.SerializeObject(other_updates, bw);
            ObjectUtils.SerializeObject(chats, bw);
            ObjectUtils.SerializeObject(users, bw);

        }
    }
}
