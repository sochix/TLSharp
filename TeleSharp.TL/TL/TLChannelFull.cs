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

        public int flags { get; set; }
        public bool can_view_participants { get; set; }
        public bool can_set_username { get; set; }
        public int id { get; set; }
        public string about { get; set; }
        public int? participants_count { get; set; }
        public int? admins_count { get; set; }
        public int? kicked_count { get; set; }
        public int read_inbox_max_id { get; set; }
        public int read_outbox_max_id { get; set; }
        public int unread_count { get; set; }
        public TLAbsPhoto chat_photo { get; set; }
        public TLAbsPeerNotifySettings notify_settings { get; set; }
        public TLAbsExportedChatInvite exported_invite { get; set; }
        public TLVector<TLBotInfo> bot_info { get; set; }
        public int? migrated_from_chat_id { get; set; }
        public int? migrated_from_max_id { get; set; }
        public int? pinned_msg_id { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = can_view_participants ? (flags | 8) : (flags & ~8);
            flags = can_set_username ? (flags | 64) : (flags & ~64);
            flags = participants_count != null ? (flags | 1) : (flags & ~1);
            flags = admins_count != null ? (flags | 2) : (flags & ~2);
            flags = kicked_count != null ? (flags | 4) : (flags & ~4);
            flags = migrated_from_chat_id != null ? (flags | 16) : (flags & ~16);
            flags = migrated_from_max_id != null ? (flags | 16) : (flags & ~16);
            flags = pinned_msg_id != null ? (flags | 32) : (flags & ~32);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            can_view_participants = (flags & 8) != 0;
            can_set_username = (flags & 64) != 0;
            id = br.ReadInt32();
            about = StringUtil.Deserialize(br);
            if ((flags & 1) != 0)
                participants_count = br.ReadInt32();
            else
                participants_count = null;

            if ((flags & 2) != 0)
                admins_count = br.ReadInt32();
            else
                admins_count = null;

            if ((flags & 4) != 0)
                kicked_count = br.ReadInt32();
            else
                kicked_count = null;

            read_inbox_max_id = br.ReadInt32();
            read_outbox_max_id = br.ReadInt32();
            unread_count = br.ReadInt32();
            chat_photo = (TLAbsPhoto)ObjectUtils.DeserializeObject(br);
            notify_settings = (TLAbsPeerNotifySettings)ObjectUtils.DeserializeObject(br);
            exported_invite = (TLAbsExportedChatInvite)ObjectUtils.DeserializeObject(br);
            bot_info = (TLVector<TLBotInfo>)ObjectUtils.DeserializeVector<TLBotInfo>(br);
            if ((flags & 16) != 0)
                migrated_from_chat_id = br.ReadInt32();
            else
                migrated_from_chat_id = null;

            if ((flags & 16) != 0)
                migrated_from_max_id = br.ReadInt32();
            else
                migrated_from_max_id = null;

            if ((flags & 32) != 0)
                pinned_msg_id = br.ReadInt32();
            else
                pinned_msg_id = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);


            bw.Write(id);
            StringUtil.Serialize(about, bw);
            if ((flags & 1) != 0)
                bw.Write(participants_count.Value);
            if ((flags & 2) != 0)
                bw.Write(admins_count.Value);
            if ((flags & 4) != 0)
                bw.Write(kicked_count.Value);
            bw.Write(read_inbox_max_id);
            bw.Write(read_outbox_max_id);
            bw.Write(unread_count);
            ObjectUtils.SerializeObject(chat_photo, bw);
            ObjectUtils.SerializeObject(notify_settings, bw);
            ObjectUtils.SerializeObject(exported_invite, bw);
            ObjectUtils.SerializeObject(bot_info, bw);
            if ((flags & 16) != 0)
                bw.Write(migrated_from_chat_id.Value);
            if ((flags & 16) != 0)
                bw.Write(migrated_from_max_id.Value);
            if ((flags & 32) != 0)
                bw.Write(pinned_msg_id.Value);

        }
    }
}
