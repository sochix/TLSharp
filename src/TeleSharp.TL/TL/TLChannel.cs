using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-753232354)]
    public class TLChannel : TLAbsChat
    {
        public override int Constructor
        {
            get
            {
                return -753232354;
            }
        }

        public int Flags { get; set; }
        public bool Creator { get; set; }
        public bool Left { get; set; }
        public bool Broadcast { get; set; }
        public bool Verified { get; set; }
        public bool Megagroup { get; set; }
        public bool Restricted { get; set; }
        public bool Signatures { get; set; }
        public bool Min { get; set; }
        public bool Scam { get; set; }
        public bool HasLink { get; set; }
        public bool HasGeo { get; set; }
        public bool SlowmodeEnabled { get; set; }
        public int Id { get; set; }
        public long? AccessHash { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public TLAbsChatPhoto Photo { get; set; }
        public int Date { get; set; }
        public int Version { get; set; }
        public TLVector<TLRestrictionReason> RestrictionReason { get; set; }
        public TLChatAdminRights AdminRights { get; set; }
        public TLChatBannedRights BannedRights { get; set; }
        public TLChatBannedRights DefaultBannedRights { get; set; }
        public int? ParticipantsCount { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Creator = (Flags & 1) != 0;
            Left = (Flags & 4) != 0;
            Broadcast = (Flags & 32) != 0;
            Verified = (Flags & 128) != 0;
            Megagroup = (Flags & 256) != 0;
            Restricted = (Flags & 512) != 0;
            Signatures = (Flags & 2048) != 0;
            Min = (Flags & 4096) != 0;
            Scam = (Flags & 524288) != 0;
            HasLink = (Flags & 1048576) != 0;
            HasGeo = (Flags & 2097152) != 0;
            SlowmodeEnabled = (Flags & 4194304) != 0;
            Id = br.ReadInt32();
            if ((Flags & 8192) != 0)
                AccessHash = br.ReadInt64();
            else
                AccessHash = null;

            Title = StringUtil.Deserialize(br);
            if ((Flags & 64) != 0)
                Username = StringUtil.Deserialize(br);
            else
                Username = null;

            Photo = (TLAbsChatPhoto)ObjectUtils.DeserializeObject(br);
            Date = br.ReadInt32();
            Version = br.ReadInt32();
            if ((Flags & 512) != 0)
                RestrictionReason = (TLVector<TLRestrictionReason>)ObjectUtils.DeserializeVector<TLRestrictionReason>(br);
            else
                RestrictionReason = null;

            if ((Flags & 16384) != 0)
                AdminRights = (TLChatAdminRights)ObjectUtils.DeserializeObject(br);
            else
                AdminRights = null;

            if ((Flags & 32768) != 0)
                BannedRights = (TLChatBannedRights)ObjectUtils.DeserializeObject(br);
            else
                BannedRights = null;

            if ((Flags & 262144) != 0)
                DefaultBannedRights = (TLChatBannedRights)ObjectUtils.DeserializeObject(br);
            else
                DefaultBannedRights = null;

            if ((Flags & 131072) != 0)
                ParticipantsCount = br.ReadInt32();
            else
                ParticipantsCount = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);












            bw.Write(Id);
            if ((Flags & 8192) != 0)
                bw.Write(AccessHash.Value);
            StringUtil.Serialize(Title, bw);
            if ((Flags & 64) != 0)
                StringUtil.Serialize(Username, bw);
            ObjectUtils.SerializeObject(Photo, bw);
            bw.Write(Date);
            bw.Write(Version);
            if ((Flags & 512) != 0)
                ObjectUtils.SerializeObject(RestrictionReason, bw);
            if ((Flags & 16384) != 0)
                ObjectUtils.SerializeObject(AdminRights, bw);
            if ((Flags & 32768) != 0)
                ObjectUtils.SerializeObject(BannedRights, bw);
            if ((Flags & 262144) != 0)
                ObjectUtils.SerializeObject(DefaultBannedRights, bw);
            if ((Flags & 131072) != 0)
                bw.Write(ParticipantsCount.Value);

        }
    }
}
