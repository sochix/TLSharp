using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(773059779)]
    public class TLUser : TLAbsUser
    {
        public override int Constructor
        {
            get
            {
                return 773059779;
            }
        }

        public int flags { get; set; }
        public bool self { get; set; }
        public bool contact { get; set; }
        public bool mutual_contact { get; set; }
        public bool deleted { get; set; }
        public bool bot { get; set; }
        public bool bot_chat_history { get; set; }
        public bool bot_nochats { get; set; }
        public bool verified { get; set; }
        public bool restricted { get; set; }
        public bool min { get; set; }
        public bool bot_inline_geo { get; set; }
        public int id { get; set; }
        public long? access_hash { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
        public string phone { get; set; }
        public TLAbsUserProfilePhoto photo { get; set; }
        public TLAbsUserStatus status { get; set; }
        public int? bot_info_version { get; set; }
        public string restriction_reason { get; set; }
        public string bot_inline_placeholder { get; set; }
        public string lang_code { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = self ? (flags | 1024) : (flags & ~1024);
            flags = contact ? (flags | 2048) : (flags & ~2048);
            flags = mutual_contact ? (flags | 4096) : (flags & ~4096);
            flags = deleted ? (flags | 8192) : (flags & ~8192);
            flags = bot ? (flags | 16384) : (flags & ~16384);
            flags = bot_chat_history ? (flags | 32768) : (flags & ~32768);
            flags = bot_nochats ? (flags | 65536) : (flags & ~65536);
            flags = verified ? (flags | 131072) : (flags & ~131072);
            flags = restricted ? (flags | 262144) : (flags & ~262144);
            flags = min ? (flags | 1048576) : (flags & ~1048576);
            flags = bot_inline_geo ? (flags | 2097152) : (flags & ~2097152);
            flags = access_hash != null ? (flags | 1) : (flags & ~1);
            flags = first_name != null ? (flags | 2) : (flags & ~2);
            flags = last_name != null ? (flags | 4) : (flags & ~4);
            flags = username != null ? (flags | 8) : (flags & ~8);
            flags = phone != null ? (flags | 16) : (flags & ~16);
            flags = photo != null ? (flags | 32) : (flags & ~32);
            flags = status != null ? (flags | 64) : (flags & ~64);
            flags = bot_info_version != null ? (flags | 16384) : (flags & ~16384);
            flags = restriction_reason != null ? (flags | 262144) : (flags & ~262144);
            flags = bot_inline_placeholder != null ? (flags | 524288) : (flags & ~524288);
            flags = lang_code != null ? (flags | 4194304) : (flags & ~4194304);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            self = (flags & 1024) != 0;
            contact = (flags & 2048) != 0;
            mutual_contact = (flags & 4096) != 0;
            deleted = (flags & 8192) != 0;
            bot = (flags & 16384) != 0;
            bot_chat_history = (flags & 32768) != 0;
            bot_nochats = (flags & 65536) != 0;
            verified = (flags & 131072) != 0;
            restricted = (flags & 262144) != 0;
            min = (flags & 1048576) != 0;
            bot_inline_geo = (flags & 2097152) != 0;
            id = br.ReadInt32();
            if ((flags & 1) != 0)
                access_hash = br.ReadInt64();
            else
                access_hash = null;

            if ((flags & 2) != 0)
                first_name = StringUtil.Deserialize(br);
            else
                first_name = null;

            if ((flags & 4) != 0)
                last_name = StringUtil.Deserialize(br);
            else
                last_name = null;

            if ((flags & 8) != 0)
                username = StringUtil.Deserialize(br);
            else
                username = null;

            if ((flags & 16) != 0)
                phone = StringUtil.Deserialize(br);
            else
                phone = null;

            if ((flags & 32) != 0)
                photo = (TLAbsUserProfilePhoto)ObjectUtils.DeserializeObject(br);
            else
                photo = null;

            if ((flags & 64) != 0)
                status = (TLAbsUserStatus)ObjectUtils.DeserializeObject(br);
            else
                status = null;

            if ((flags & 16384) != 0)
                bot_info_version = br.ReadInt32();
            else
                bot_info_version = null;

            if ((flags & 262144) != 0)
                restriction_reason = StringUtil.Deserialize(br);
            else
                restriction_reason = null;

            if ((flags & 524288) != 0)
                bot_inline_placeholder = StringUtil.Deserialize(br);
            else
                bot_inline_placeholder = null;

            if ((flags & 4194304) != 0)
                lang_code = StringUtil.Deserialize(br);
            else
                lang_code = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);











            bw.Write(id);
            if ((flags & 1) != 0)
                bw.Write(access_hash.Value);
            if ((flags & 2) != 0)
                StringUtil.Serialize(first_name, bw);
            if ((flags & 4) != 0)
                StringUtil.Serialize(last_name, bw);
            if ((flags & 8) != 0)
                StringUtil.Serialize(username, bw);
            if ((flags & 16) != 0)
                StringUtil.Serialize(phone, bw);
            if ((flags & 32) != 0)
                ObjectUtils.SerializeObject(photo, bw);
            if ((flags & 64) != 0)
                ObjectUtils.SerializeObject(status, bw);
            if ((flags & 16384) != 0)
                bw.Write(bot_info_version.Value);
            if ((flags & 262144) != 0)
                StringUtil.Serialize(restriction_reason, bw);
            if ((flags & 524288) != 0)
                StringUtil.Serialize(bot_inline_placeholder, bw);
            if ((flags & 4194304) != 0)
                StringUtil.Serialize(lang_code, bw);

        }
    }
}
