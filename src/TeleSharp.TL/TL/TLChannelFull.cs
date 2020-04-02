using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1009430225)]
    public class TLChannelFull : TLAbsChatFull
    {
        public override int Constructor
        {
            get
            {
                return -1009430225;
            }
        }

        public int Flags { get; set; }
        public bool CanViewParticipants { get; set; }
        public bool CanSetUsername { get; set; }
        public int Id { get; set; }
        public string About { get; set; }
        public int? ParticipantsCount { get; set; }
        public int? AdminsCount { get; set; }
        public int? KickedCount { get; set; }
        public int ReadInboxMaxId { get; set; }
        public int ReadOutboxMaxId { get; set; }
        public int UnreadCount { get; set; }
        public TLAbsPhoto ChatPhoto { get; set; }
        public TLAbsPeerNotifySettings NotifySettings { get; set; }
        public TLAbsExportedChatInvite ExportedInvite { get; set; }
        public TLVector<TLBotInfo> BotInfo { get; set; }
        public int? MigratedFromChatId { get; set; }
        public int? MigratedFromMaxId { get; set; }
        public int? PinnedMsgId { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = CanViewParticipants ? (Flags | 8) : (Flags & ~8);
            Flags = CanSetUsername ? (Flags | 64) : (Flags & ~64);
            Flags = ParticipantsCount != null ? (Flags | 1) : (Flags & ~1);
            Flags = AdminsCount != null ? (Flags | 2) : (Flags & ~2);
            Flags = KickedCount != null ? (Flags | 4) : (Flags & ~4);
            Flags = MigratedFromChatId != null ? (Flags | 16) : (Flags & ~16);
            Flags = MigratedFromMaxId != null ? (Flags | 16) : (Flags & ~16);
            Flags = PinnedMsgId != null ? (Flags | 32) : (Flags & ~32);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            CanViewParticipants = (Flags & 8) != 0;
            CanSetUsername = (Flags & 64) != 0;
            Id = br.ReadInt32();
            About = StringUtil.Deserialize(br);
            if ((Flags & 1) != 0)
                ParticipantsCount = br.ReadInt32();
            else
                ParticipantsCount = null;

            if ((Flags & 2) != 0)
                AdminsCount = br.ReadInt32();
            else
                AdminsCount = null;

            if ((Flags & 4) != 0)
                KickedCount = br.ReadInt32();
            else
                KickedCount = null;

            ReadInboxMaxId = br.ReadInt32();
            ReadOutboxMaxId = br.ReadInt32();
            UnreadCount = br.ReadInt32();
            ChatPhoto = (TLAbsPhoto)ObjectUtils.DeserializeObject(br);
            NotifySettings = (TLAbsPeerNotifySettings)ObjectUtils.DeserializeObject(br);
            ExportedInvite = (TLAbsExportedChatInvite)ObjectUtils.DeserializeObject(br);
            BotInfo = (TLVector<TLBotInfo>)ObjectUtils.DeserializeVector<TLBotInfo>(br);
            if ((Flags & 16) != 0)
                MigratedFromChatId = br.ReadInt32();
            else
                MigratedFromChatId = null;

            if ((Flags & 16) != 0)
                MigratedFromMaxId = br.ReadInt32();
            else
                MigratedFromMaxId = null;

            if ((Flags & 32) != 0)
                PinnedMsgId = br.ReadInt32();
            else
                PinnedMsgId = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);


            bw.Write(Id);
            StringUtil.Serialize(About, bw);
            if ((Flags & 1) != 0)
                bw.Write(ParticipantsCount.Value);
            if ((Flags & 2) != 0)
                bw.Write(AdminsCount.Value);
            if ((Flags & 4) != 0)
                bw.Write(KickedCount.Value);
            bw.Write(ReadInboxMaxId);
            bw.Write(ReadOutboxMaxId);
            bw.Write(UnreadCount);
            ObjectUtils.SerializeObject(ChatPhoto, bw);
            ObjectUtils.SerializeObject(NotifySettings, bw);
            ObjectUtils.SerializeObject(ExportedInvite, bw);
            ObjectUtils.SerializeObject(BotInfo, bw);
            if ((Flags & 16) != 0)
                bw.Write(MigratedFromChatId.Value);
            if ((Flags & 16) != 0)
                bw.Write(MigratedFromMaxId.Value);
            if ((Flags & 32) != 0)
                bw.Write(PinnedMsgId.Value);

        }
    }
}
