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

        public int Flags { get; set; }
        public bool Final { get; set; }
        public int Pts { get; set; }
        public int? Timeout { get; set; }
        public int TopMessage { get; set; }
        public int ReadInboxMaxId { get; set; }
        public int ReadOutboxMaxId { get; set; }
        public int UnreadCount { get; set; }
        public TLVector<TLAbsMessage> Messages { get; set; }
        public TLVector<TLAbsChat> Chats { get; set; }
        public TLVector<TLAbsUser> Users { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Final ? (Flags | 1) : (Flags & ~1);
            Flags = Timeout != null ? (Flags | 2) : (Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Final = (Flags & 1) != 0;
            Pts = br.ReadInt32();
            if ((Flags & 2) != 0)
                Timeout = br.ReadInt32();
            else
                Timeout = null;

            TopMessage = br.ReadInt32();
            ReadInboxMaxId = br.ReadInt32();
            ReadOutboxMaxId = br.ReadInt32();
            UnreadCount = br.ReadInt32();
            Messages = (TLVector<TLAbsMessage>)ObjectUtils.DeserializeVector<TLAbsMessage>(br);
            Chats = (TLVector<TLAbsChat>)ObjectUtils.DeserializeVector<TLAbsChat>(br);
            Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);

            bw.Write(Pts);
            if ((Flags & 2) != 0)
                bw.Write(Timeout.Value);
            bw.Write(TopMessage);
            bw.Write(ReadInboxMaxId);
            bw.Write(ReadOutboxMaxId);
            bw.Write(UnreadCount);
            ObjectUtils.SerializeObject(Messages, bw);
            ObjectUtils.SerializeObject(Chats, bw);
            ObjectUtils.SerializeObject(Users, bw);

        }
    }
}
