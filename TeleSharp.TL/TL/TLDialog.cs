using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-455150117)]
    public class TLDialog : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -455150117;
            }
        }

        public TLAbsDraftMessage Draft { get; set; }

        public int Flags { get; set; }

        public TLAbsPeerNotifySettings NotifySettings { get; set; }

        public TLAbsPeer Peer { get; set; }

        public bool Pinned { get; set; }

        public int? Pts { get; set; }

        public int ReadInboxMaxId { get; set; }

        public int ReadOutboxMaxId { get; set; }

        public int TopMessage { get; set; }

        public int UnreadCount { get; set; }

        public int UnreadMentionsCount { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Pinned = (Flags & 4) != 0;
            Peer = (TLAbsPeer)ObjectUtils.DeserializeObject(br);
            TopMessage = br.ReadInt32();
            ReadInboxMaxId = br.ReadInt32();
            ReadOutboxMaxId = br.ReadInt32();
            UnreadCount = br.ReadInt32();
            UnreadMentionsCount = br.ReadInt32();
            NotifySettings = (TLAbsPeerNotifySettings)ObjectUtils.DeserializeObject(br);
            if ((Flags & 1) != 0)
                Pts = br.ReadInt32();
            else
                Pts = null;

            if ((Flags & 2) != 0)
                Draft = (TLAbsDraftMessage)ObjectUtils.DeserializeObject(br);
            else
                Draft = null;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            ObjectUtils.SerializeObject(Peer, bw);
            bw.Write(TopMessage);
            bw.Write(ReadInboxMaxId);
            bw.Write(ReadOutboxMaxId);
            bw.Write(UnreadCount);
            bw.Write(UnreadMentionsCount);
            ObjectUtils.SerializeObject(NotifySettings, bw);
            if ((Flags & 1) != 0)
                bw.Write(Pts.Value);
            if ((Flags & 2) != 0)
                ObjectUtils.SerializeObject(Draft, bw);
        }
    }
}
