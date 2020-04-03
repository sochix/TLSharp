using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-302941166)]
    public class TLUserFull : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -302941166;
            }
        }

        public int Flags { get; set; }
        public bool Blocked { get; set; }
        public bool PhoneCallsAvailable { get; set; }
        public bool PhoneCallsPrivate { get; set; }
        public bool CanPinMessage { get; set; }
        public bool HasScheduled { get; set; }
        public TLAbsUser User { get; set; }
        public string About { get; set; }
        public TLPeerSettings Settings { get; set; }
        public TLAbsPhoto ProfilePhoto { get; set; }
        public TLPeerNotifySettings NotifySettings { get; set; }
        public TLBotInfo BotInfo { get; set; }
        public int? PinnedMsgId { get; set; }
        public int CommonChatsCount { get; set; }
        public int? FolderId { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Blocked = (Flags & 1) != 0;
            PhoneCallsAvailable = (Flags & 16) != 0;
            PhoneCallsPrivate = (Flags & 32) != 0;
            CanPinMessage = (Flags & 128) != 0;
            HasScheduled = (Flags & 4096) != 0;
            User = (TLAbsUser)ObjectUtils.DeserializeObject(br);
            if ((Flags & 2) != 0)
                About = StringUtil.Deserialize(br);
            else
                About = null;

            Settings = (TLPeerSettings)ObjectUtils.DeserializeObject(br);
            if ((Flags & 4) != 0)
                ProfilePhoto = (TLAbsPhoto)ObjectUtils.DeserializeObject(br);
            else
                ProfilePhoto = null;

            NotifySettings = (TLPeerNotifySettings)ObjectUtils.DeserializeObject(br);
            if ((Flags & 8) != 0)
                BotInfo = (TLBotInfo)ObjectUtils.DeserializeObject(br);
            else
                BotInfo = null;

            if ((Flags & 64) != 0)
                PinnedMsgId = br.ReadInt32();
            else
                PinnedMsgId = null;

            CommonChatsCount = br.ReadInt32();
            if ((Flags & 2048) != 0)
                FolderId = br.ReadInt32();
            else
                FolderId = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);





            ObjectUtils.SerializeObject(User, bw);
            if ((Flags & 2) != 0)
                StringUtil.Serialize(About, bw);
            ObjectUtils.SerializeObject(Settings, bw);
            if ((Flags & 4) != 0)
                ObjectUtils.SerializeObject(ProfilePhoto, bw);
            ObjectUtils.SerializeObject(NotifySettings, bw);
            if ((Flags & 8) != 0)
                ObjectUtils.SerializeObject(BotInfo, bw);
            if ((Flags & 64) != 0)
                bw.Write(PinnedMsgId.Value);
            bw.Write(CommonChatsCount);
            if ((Flags & 2048) != 0)
                bw.Write(FolderId.Value);

        }
    }
}
