using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(856375399)]
    public class TLConfig : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 856375399;
            }
        }

        public int Flags { get; set; }
        public bool PhonecallsEnabled { get; set; }
        public bool DefaultP2pContacts { get; set; }
        public bool PreloadFeaturedStickers { get; set; }
        public bool IgnorePhoneEntities { get; set; }
        public bool RevokePmInbox { get; set; }
        public bool BlockedMode { get; set; }
        public bool PfsEnabled { get; set; }
        public int Date { get; set; }
        public int Expires { get; set; }
        public bool TestMode { get; set; }
        public int ThisDc { get; set; }
        public TLVector<TLDcOption> DcOptions { get; set; }
        public string DcTxtDomainName { get; set; }
        public int ChatSizeMax { get; set; }
        public int MegagroupSizeMax { get; set; }
        public int ForwardedCountMax { get; set; }
        public int OnlineUpdatePeriodMs { get; set; }
        public int OfflineBlurTimeoutMs { get; set; }
        public int OfflineIdleTimeoutMs { get; set; }
        public int OnlineCloudTimeoutMs { get; set; }
        public int NotifyCloudDelayMs { get; set; }
        public int NotifyDefaultDelayMs { get; set; }
        public int PushChatPeriodMs { get; set; }
        public int PushChatLimit { get; set; }
        public int SavedGifsLimit { get; set; }
        public int EditTimeLimit { get; set; }
        public int RevokeTimeLimit { get; set; }
        public int RevokePmTimeLimit { get; set; }
        public int RatingEDecay { get; set; }
        public int StickersRecentLimit { get; set; }
        public int StickersFavedLimit { get; set; }
        public int ChannelsReadMediaPeriod { get; set; }
        public int? TmpSessions { get; set; }
        public int PinnedDialogsCountMax { get; set; }
        public int PinnedInfolderCountMax { get; set; }
        public int CallReceiveTimeoutMs { get; set; }
        public int CallRingTimeoutMs { get; set; }
        public int CallConnectTimeoutMs { get; set; }
        public int CallPacketTimeoutMs { get; set; }
        public string MeUrlPrefix { get; set; }
        public string AutoupdateUrlPrefix { get; set; }
        public string GifSearchUsername { get; set; }
        public string VenueSearchUsername { get; set; }
        public string ImgSearchUsername { get; set; }
        public string StaticMapsProvider { get; set; }
        public int CaptionLengthMax { get; set; }
        public int MessageLengthMax { get; set; }
        public int WebfileDcId { get; set; }
        public string SuggestedLangCode { get; set; }
        public int? LangPackVersion { get; set; }
        public int? BaseLangPackVersion { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            PhonecallsEnabled = (Flags & 2) != 0;
            DefaultP2pContacts = (Flags & 8) != 0;
            PreloadFeaturedStickers = (Flags & 16) != 0;
            IgnorePhoneEntities = (Flags & 32) != 0;
            RevokePmInbox = (Flags & 64) != 0;
            BlockedMode = (Flags & 256) != 0;
            PfsEnabled = (Flags & 8192) != 0;
            Date = br.ReadInt32();
            Expires = br.ReadInt32();
            TestMode = BoolUtil.Deserialize(br);
            ThisDc = br.ReadInt32();
            DcOptions = (TLVector<TLDcOption>)ObjectUtils.DeserializeVector<TLDcOption>(br);
            DcTxtDomainName = StringUtil.Deserialize(br);
            ChatSizeMax = br.ReadInt32();
            MegagroupSizeMax = br.ReadInt32();
            ForwardedCountMax = br.ReadInt32();
            OnlineUpdatePeriodMs = br.ReadInt32();
            OfflineBlurTimeoutMs = br.ReadInt32();
            OfflineIdleTimeoutMs = br.ReadInt32();
            OnlineCloudTimeoutMs = br.ReadInt32();
            NotifyCloudDelayMs = br.ReadInt32();
            NotifyDefaultDelayMs = br.ReadInt32();
            PushChatPeriodMs = br.ReadInt32();
            PushChatLimit = br.ReadInt32();
            SavedGifsLimit = br.ReadInt32();
            EditTimeLimit = br.ReadInt32();
            RevokeTimeLimit = br.ReadInt32();
            RevokePmTimeLimit = br.ReadInt32();
            RatingEDecay = br.ReadInt32();
            StickersRecentLimit = br.ReadInt32();
            StickersFavedLimit = br.ReadInt32();
            ChannelsReadMediaPeriod = br.ReadInt32();
            if ((Flags & 1) != 0)
                TmpSessions = br.ReadInt32();
            else
                TmpSessions = null;

            PinnedDialogsCountMax = br.ReadInt32();
            PinnedInfolderCountMax = br.ReadInt32();
            CallReceiveTimeoutMs = br.ReadInt32();
            CallRingTimeoutMs = br.ReadInt32();
            CallConnectTimeoutMs = br.ReadInt32();
            CallPacketTimeoutMs = br.ReadInt32();
            MeUrlPrefix = StringUtil.Deserialize(br);
            if ((Flags & 128) != 0)
                AutoupdateUrlPrefix = StringUtil.Deserialize(br);
            else
                AutoupdateUrlPrefix = null;

            if ((Flags & 512) != 0)
                GifSearchUsername = StringUtil.Deserialize(br);
            else
                GifSearchUsername = null;

            if ((Flags & 1024) != 0)
                VenueSearchUsername = StringUtil.Deserialize(br);
            else
                VenueSearchUsername = null;

            if ((Flags & 2048) != 0)
                ImgSearchUsername = StringUtil.Deserialize(br);
            else
                ImgSearchUsername = null;

            if ((Flags & 4096) != 0)
                StaticMapsProvider = StringUtil.Deserialize(br);
            else
                StaticMapsProvider = null;

            CaptionLengthMax = br.ReadInt32();
            MessageLengthMax = br.ReadInt32();
            WebfileDcId = br.ReadInt32();
            if ((Flags & 4) != 0)
                SuggestedLangCode = StringUtil.Deserialize(br);
            else
                SuggestedLangCode = null;

            if ((Flags & 4) != 0)
                LangPackVersion = br.ReadInt32();
            else
                LangPackVersion = null;

            if ((Flags & 4) != 0)
                BaseLangPackVersion = br.ReadInt32();
            else
                BaseLangPackVersion = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);







            bw.Write(Date);
            bw.Write(Expires);
            BoolUtil.Serialize(TestMode, bw);
            bw.Write(ThisDc);
            ObjectUtils.SerializeObject(DcOptions, bw);
            StringUtil.Serialize(DcTxtDomainName, bw);
            bw.Write(ChatSizeMax);
            bw.Write(MegagroupSizeMax);
            bw.Write(ForwardedCountMax);
            bw.Write(OnlineUpdatePeriodMs);
            bw.Write(OfflineBlurTimeoutMs);
            bw.Write(OfflineIdleTimeoutMs);
            bw.Write(OnlineCloudTimeoutMs);
            bw.Write(NotifyCloudDelayMs);
            bw.Write(NotifyDefaultDelayMs);
            bw.Write(PushChatPeriodMs);
            bw.Write(PushChatLimit);
            bw.Write(SavedGifsLimit);
            bw.Write(EditTimeLimit);
            bw.Write(RevokeTimeLimit);
            bw.Write(RevokePmTimeLimit);
            bw.Write(RatingEDecay);
            bw.Write(StickersRecentLimit);
            bw.Write(StickersFavedLimit);
            bw.Write(ChannelsReadMediaPeriod);
            if ((Flags & 1) != 0)
                bw.Write(TmpSessions.Value);
            bw.Write(PinnedDialogsCountMax);
            bw.Write(PinnedInfolderCountMax);
            bw.Write(CallReceiveTimeoutMs);
            bw.Write(CallRingTimeoutMs);
            bw.Write(CallConnectTimeoutMs);
            bw.Write(CallPacketTimeoutMs);
            StringUtil.Serialize(MeUrlPrefix, bw);
            if ((Flags & 128) != 0)
                StringUtil.Serialize(AutoupdateUrlPrefix, bw);
            if ((Flags & 512) != 0)
                StringUtil.Serialize(GifSearchUsername, bw);
            if ((Flags & 1024) != 0)
                StringUtil.Serialize(VenueSearchUsername, bw);
            if ((Flags & 2048) != 0)
                StringUtil.Serialize(ImgSearchUsername, bw);
            if ((Flags & 4096) != 0)
                StringUtil.Serialize(StaticMapsProvider, bw);
            bw.Write(CaptionLengthMax);
            bw.Write(MessageLengthMax);
            bw.Write(WebfileDcId);
            if ((Flags & 4) != 0)
                StringUtil.Serialize(SuggestedLangCode, bw);
            if ((Flags & 4) != 0)
                bw.Write(LangPackVersion.Value);
            if ((Flags & 4) != 0)
                bw.Write(BaseLangPackVersion.Value);

        }
    }
}
