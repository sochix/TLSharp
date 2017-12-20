using System.IO;

namespace TeleSharp.TL
{
    [TLObject(253890367)]
    public class TLUserFull : TLObject
    {
        public string About { get; set; }

        public bool Blocked { get; set; }

        public TLBotInfo BotInfo { get; set; }

        public int CommonChatsCount { get; set; }

        public override int Constructor
        {
            get
            {
                return 253890367;
            }
        }

        public int Flags { get; set; }

        public Contacts.TLLink Link { get; set; }

        public TLAbsPeerNotifySettings NotifySettings { get; set; }

        public bool PhoneCallsAvailable { get; set; }

        public bool PhoneCallsPrivate { get; set; }

        public TLAbsPhoto ProfilePhoto { get; set; }

        public TLAbsUser User { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Blocked = (Flags & 1) != 0;
            PhoneCallsAvailable = (Flags & 16) != 0;
            PhoneCallsPrivate = (Flags & 32) != 0;
            User = (TLAbsUser)ObjectUtils.DeserializeObject(br);
            if ((Flags & 2) != 0)
                About = StringUtil.Deserialize(br);
            else
                About = null;

            Link = (Contacts.TLLink)ObjectUtils.DeserializeObject(br);
            if ((Flags & 4) != 0)
                ProfilePhoto = (TLAbsPhoto)ObjectUtils.DeserializeObject(br);
            else
                ProfilePhoto = null;

            NotifySettings = (TLAbsPeerNotifySettings)ObjectUtils.DeserializeObject(br);
            if ((Flags & 8) != 0)
                BotInfo = (TLBotInfo)ObjectUtils.DeserializeObject(br);
            else
                BotInfo = null;

            CommonChatsCount = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);



            ObjectUtils.SerializeObject(User, bw);
            if ((Flags & 2) != 0)
                StringUtil.Serialize(About, bw);
            ObjectUtils.SerializeObject(Link, bw);
            if ((Flags & 4) != 0)
                ObjectUtils.SerializeObject(ProfilePhoto, bw);
            ObjectUtils.SerializeObject(NotifySettings, bw);
            if ((Flags & 8) != 0)
                ObjectUtils.SerializeObject(BotInfo, bw);
            bw.Write(CommonChatsCount);
        }
    }
}
