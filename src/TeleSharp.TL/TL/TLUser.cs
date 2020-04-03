using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1820043071)]
    public class TLUser : TLAbsUser
    {
        public override int Constructor
        {
            get
            {
                return -1820043071;
            }
        }

        public int Flags { get; set; }
        public bool Self { get; set; }
        public bool Contact { get; set; }
        public bool MutualContact { get; set; }
        public bool Deleted { get; set; }
        public bool Bot { get; set; }
        public bool BotChatHistory { get; set; }
        public bool BotNochats { get; set; }
        public bool Verified { get; set; }
        public bool Restricted { get; set; }
        public bool Min { get; set; }
        public bool BotInlineGeo { get; set; }
        public bool Support { get; set; }
        public bool Scam { get; set; }
        public int Id { get; set; }
        public long? AccessHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public TLAbsUserProfilePhoto Photo { get; set; }
        public TLAbsUserStatus Status { get; set; }
        public int? BotInfoVersion { get; set; }
        public TLVector<TLRestrictionReason> RestrictionReason { get; set; }
        public string BotInlinePlaceholder { get; set; }
        public string LangCode { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Self = (Flags & 1024) != 0;
            Contact = (Flags & 2048) != 0;
            MutualContact = (Flags & 4096) != 0;
            Deleted = (Flags & 8192) != 0;
            Bot = (Flags & 16384) != 0;
            BotChatHistory = (Flags & 32768) != 0;
            BotNochats = (Flags & 65536) != 0;
            Verified = (Flags & 131072) != 0;
            Restricted = (Flags & 262144) != 0;
            Min = (Flags & 1048576) != 0;
            BotInlineGeo = (Flags & 2097152) != 0;
            Support = (Flags & 8388608) != 0;
            Scam = (Flags & 16777216) != 0;
            Id = br.ReadInt32();
            if ((Flags & 1) != 0)
                AccessHash = br.ReadInt64();
            else
                AccessHash = null;

            if ((Flags & 2) != 0)
                FirstName = StringUtil.Deserialize(br);
            else
                FirstName = null;

            if ((Flags & 4) != 0)
                LastName = StringUtil.Deserialize(br);
            else
                LastName = null;

            if ((Flags & 8) != 0)
                Username = StringUtil.Deserialize(br);
            else
                Username = null;

            if ((Flags & 16) != 0)
                Phone = StringUtil.Deserialize(br);
            else
                Phone = null;

            if ((Flags & 32) != 0)
                Photo = (TLAbsUserProfilePhoto)ObjectUtils.DeserializeObject(br);
            else
                Photo = null;

            if ((Flags & 64) != 0)
                Status = (TLAbsUserStatus)ObjectUtils.DeserializeObject(br);
            else
                Status = null;

            if ((Flags & 16384) != 0)
                BotInfoVersion = br.ReadInt32();
            else
                BotInfoVersion = null;

            if ((Flags & 262144) != 0)
                RestrictionReason = (TLVector<TLRestrictionReason>)ObjectUtils.DeserializeVector<TLRestrictionReason>(br);
            else
                RestrictionReason = null;

            if ((Flags & 524288) != 0)
                BotInlinePlaceholder = StringUtil.Deserialize(br);
            else
                BotInlinePlaceholder = null;

            if ((Flags & 4194304) != 0)
                LangCode = StringUtil.Deserialize(br);
            else
                LangCode = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);













            bw.Write(Id);
            if ((Flags & 1) != 0)
                bw.Write(AccessHash.Value);
            if ((Flags & 2) != 0)
                StringUtil.Serialize(FirstName, bw);
            if ((Flags & 4) != 0)
                StringUtil.Serialize(LastName, bw);
            if ((Flags & 8) != 0)
                StringUtil.Serialize(Username, bw);
            if ((Flags & 16) != 0)
                StringUtil.Serialize(Phone, bw);
            if ((Flags & 32) != 0)
                ObjectUtils.SerializeObject(Photo, bw);
            if ((Flags & 64) != 0)
                ObjectUtils.SerializeObject(Status, bw);
            if ((Flags & 16384) != 0)
                bw.Write(BotInfoVersion.Value);
            if ((Flags & 262144) != 0)
                ObjectUtils.SerializeObject(RestrictionReason, bw);
            if ((Flags & 524288) != 0)
                StringUtil.Serialize(BotInlinePlaceholder, bw);
            if ((Flags & 4194304) != 0)
                StringUtil.Serialize(LangCode, bw);

        }
    }
}
