using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-882895228)]
    public class TLConfig : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -882895228;
            }
        }

        public int Flags { get; set; }
        public bool PhonecallsEnabled { get; set; }
        public int Date { get; set; }
        public int Expires { get; set; }
        public bool TestMode { get; set; }
        public int ThisDc { get; set; }
        public TLVector<TLDcOption> DcOptions { get; set; }
        public int ChatSizeMax { get; set; }
        public int MegagroupSizeMax { get; set; }
        public int ForwardedCountMax { get; set; }
        public int OnlineUpdatePeriodMs { get; set; }
        public int OfflineBlurTimeoutMs { get; set; }
        public int OfflineIdleTimeoutMs { get; set; }
        public int OnlineCloudTimeoutMs { get; set; }
        public int NotifyCloudDelayMs { get; set; }
        public int NotifyDefaultDelayMs { get; set; }
        public int ChatBigSize { get; set; }
        public int PushChatPeriodMs { get; set; }
        public int PushChatLimit { get; set; }
        public int SavedGifsLimit { get; set; }
        public int EditTimeLimit { get; set; }
        public int RatingEDecay { get; set; }
        public int StickersRecentLimit { get; set; }
        public int? TmpSessions { get; set; }
        public int PinnedDialogsCountMax { get; set; }
        public int CallReceiveTimeoutMs { get; set; }
        public int CallRingTimeoutMs { get; set; }
        public int CallConnectTimeoutMs { get; set; }
        public int CallPacketTimeoutMs { get; set; }
        public string MeUrlPrefix { get; set; }
        public TLVector<TLDisabledFeature> DisabledFeatures { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = PhonecallsEnabled ? (Flags | 2) : (Flags & ~2);
            Flags = TmpSessions != null ? (Flags | 1) : (Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            PhonecallsEnabled = (Flags & 2) != 0;
            Date = br.ReadInt32();
            Expires = br.ReadInt32();
            TestMode = BoolUtil.Deserialize(br);
            ThisDc = br.ReadInt32();
            DcOptions = (TLVector<TLDcOption>)ObjectUtils.DeserializeVector<TLDcOption>(br);
            ChatSizeMax = br.ReadInt32();
            MegagroupSizeMax = br.ReadInt32();
            ForwardedCountMax = br.ReadInt32();
            OnlineUpdatePeriodMs = br.ReadInt32();
            OfflineBlurTimeoutMs = br.ReadInt32();
            OfflineIdleTimeoutMs = br.ReadInt32();
            OnlineCloudTimeoutMs = br.ReadInt32();
            NotifyCloudDelayMs = br.ReadInt32();
            NotifyDefaultDelayMs = br.ReadInt32();
            ChatBigSize = br.ReadInt32();
            PushChatPeriodMs = br.ReadInt32();
            PushChatLimit = br.ReadInt32();
            SavedGifsLimit = br.ReadInt32();
            EditTimeLimit = br.ReadInt32();
            RatingEDecay = br.ReadInt32();
            StickersRecentLimit = br.ReadInt32();
            if ((Flags & 1) != 0)
                TmpSessions = br.ReadInt32();
            else
                TmpSessions = null;

            PinnedDialogsCountMax = br.ReadInt32();
            CallReceiveTimeoutMs = br.ReadInt32();
            CallRingTimeoutMs = br.ReadInt32();
            CallConnectTimeoutMs = br.ReadInt32();
            CallPacketTimeoutMs = br.ReadInt32();
            MeUrlPrefix = StringUtil.Deserialize(br);
            DisabledFeatures = (TLVector<TLDisabledFeature>)ObjectUtils.DeserializeVector<TLDisabledFeature>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);

            bw.Write(Date);
            bw.Write(Expires);
            BoolUtil.Serialize(TestMode, bw);
            bw.Write(ThisDc);
            ObjectUtils.SerializeObject(DcOptions, bw);
            bw.Write(ChatSizeMax);
            bw.Write(MegagroupSizeMax);
            bw.Write(ForwardedCountMax);
            bw.Write(OnlineUpdatePeriodMs);
            bw.Write(OfflineBlurTimeoutMs);
            bw.Write(OfflineIdleTimeoutMs);
            bw.Write(OnlineCloudTimeoutMs);
            bw.Write(NotifyCloudDelayMs);
            bw.Write(NotifyDefaultDelayMs);
            bw.Write(ChatBigSize);
            bw.Write(PushChatPeriodMs);
            bw.Write(PushChatLimit);
            bw.Write(SavedGifsLimit);
            bw.Write(EditTimeLimit);
            bw.Write(RatingEDecay);
            bw.Write(StickersRecentLimit);
            if ((Flags & 1) != 0)
                bw.Write(TmpSessions.Value);
            bw.Write(PinnedDialogsCountMax);
            bw.Write(CallReceiveTimeoutMs);
            bw.Write(CallRingTimeoutMs);
            bw.Write(CallConnectTimeoutMs);
            bw.Write(CallPacketTimeoutMs);
            StringUtil.Serialize(MeUrlPrefix, bw);
            ObjectUtils.SerializeObject(DisabledFeatures, bw);

        }
    }
}
