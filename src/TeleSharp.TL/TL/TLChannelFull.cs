using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(763976820)]
    public class TLChannelFull : TLAbsChatFull
    {
        public override int Constructor
        {
            get
            {
                return 763976820;
            }
        }

        public int Flags { get; set; }
        public bool CanViewParticipants { get; set; }
        public bool CanSetUsername { get; set; }
        public bool CanSetStickers { get; set; }
        public bool HiddenPrehistory { get; set; }
        public bool CanViewStats { get; set; }
        public bool CanSetLocation { get; set; }
        public bool HasScheduled { get; set; }
        public int Id { get; set; }
        public string About { get; set; }
        public int? ParticipantsCount { get; set; }
        public int? AdminsCount { get; set; }
        public int? KickedCount { get; set; }
        public int? BannedCount { get; set; }
        public int? OnlineCount { get; set; }
        public int ReadInboxMaxId { get; set; }
        public int ReadOutboxMaxId { get; set; }
        public int UnreadCount { get; set; }
        public TLAbsPhoto ChatPhoto { get; set; }
        public TLPeerNotifySettings NotifySettings { get; set; }
        public TLAbsExportedChatInvite ExportedInvite { get; set; }
        public TLVector<TLBotInfo> BotInfo { get; set; }
        public int? MigratedFromChatId { get; set; }
        public int? MigratedFromMaxId { get; set; }
        public int? PinnedMsgId { get; set; }
        public TLStickerSet Stickerset { get; set; }
        public int? AvailableMinId { get; set; }
        public int? FolderId { get; set; }
        public int? LinkedChatId { get; set; }
        public TLAbsChannelLocation Location { get; set; }
        public int? SlowmodeSeconds { get; set; }
        public int? SlowmodeNextSendDate { get; set; }
        public int Pts { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            CanViewParticipants = (Flags & 8) != 0;
            CanSetUsername = (Flags & 64) != 0;
            CanSetStickers = (Flags & 128) != 0;
            HiddenPrehistory = (Flags & 1024) != 0;
            CanViewStats = (Flags & 4096) != 0;
            CanSetLocation = (Flags & 65536) != 0;
            HasScheduled = (Flags & 524288) != 0;
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

            if ((Flags & 4) != 0)
                BannedCount = br.ReadInt32();
            else
                BannedCount = null;

            if ((Flags & 8192) != 0)
                OnlineCount = br.ReadInt32();
            else
                OnlineCount = null;

            ReadInboxMaxId = br.ReadInt32();
            ReadOutboxMaxId = br.ReadInt32();
            UnreadCount = br.ReadInt32();
            ChatPhoto = (TLAbsPhoto)ObjectUtils.DeserializeObject(br);
            NotifySettings = (TLPeerNotifySettings)ObjectUtils.DeserializeObject(br);
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

            if ((Flags & 256) != 0)
                Stickerset = (TLStickerSet)ObjectUtils.DeserializeObject(br);
            else
                Stickerset = null;

            if ((Flags & 512) != 0)
                AvailableMinId = br.ReadInt32();
            else
                AvailableMinId = null;

            if ((Flags & 2048) != 0)
                FolderId = br.ReadInt32();
            else
                FolderId = null;

            if ((Flags & 16384) != 0)
                LinkedChatId = br.ReadInt32();
            else
                LinkedChatId = null;

            if ((Flags & 32768) != 0)
                Location = (TLAbsChannelLocation)ObjectUtils.DeserializeObject(br);
            else
                Location = null;

            if ((Flags & 131072) != 0)
                SlowmodeSeconds = br.ReadInt32();
            else
                SlowmodeSeconds = null;

            if ((Flags & 262144) != 0)
                SlowmodeNextSendDate = br.ReadInt32();
            else
                SlowmodeNextSendDate = null;

            Pts = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);







            bw.Write(Id);
            StringUtil.Serialize(About, bw);
            if ((Flags & 1) != 0)
                bw.Write(ParticipantsCount.Value);
            if ((Flags & 2) != 0)
                bw.Write(AdminsCount.Value);
            if ((Flags & 4) != 0)
                bw.Write(KickedCount.Value);
            if ((Flags & 4) != 0)
                bw.Write(BannedCount.Value);
            if ((Flags & 8192) != 0)
                bw.Write(OnlineCount.Value);
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
            if ((Flags & 256) != 0)
                ObjectUtils.SerializeObject(Stickerset, bw);
            if ((Flags & 512) != 0)
                bw.Write(AvailableMinId.Value);
            if ((Flags & 2048) != 0)
                bw.Write(FolderId.Value);
            if ((Flags & 16384) != 0)
                bw.Write(LinkedChatId.Value);
            if ((Flags & 32768) != 0)
                ObjectUtils.SerializeObject(Location, bw);
            if ((Flags & 131072) != 0)
                bw.Write(SlowmodeSeconds.Value);
            if ((Flags & 262144) != 0)
                bw.Write(SlowmodeNextSendDate.Value);
            bw.Write(Pts);

        }
    }
}
