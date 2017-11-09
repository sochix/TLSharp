using System.IO;
namespace TeleSharp.TL
{
    [TLObject(1568467877)]
    public class TLChannelAdminRights : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1568467877;
            }
        }

        public int flags { get; set; }
        public bool change_info { get; set; }
        public bool post_messages { get; set; }
        public bool edit_messages { get; set; }
        public bool delete_messages { get; set; }
        public bool ban_users { get; set; }
        public bool invite_users { get; set; }
        public bool invite_link { get; set; }
        public bool pin_messages { get; set; }
        public bool add_admins { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = change_info ? (flags | 1) : (flags & ~1);
            flags = post_messages ? (flags | 2) : (flags & ~2);
            flags = edit_messages ? (flags | 4) : (flags & ~4);
            flags = delete_messages ? (flags | 8) : (flags & ~8);
            flags = ban_users ? (flags | 16) : (flags & ~16);
            flags = invite_users ? (flags | 32) : (flags & ~32);
            flags = invite_link ? (flags | 64) : (flags & ~64);
            flags = pin_messages ? (flags | 128) : (flags & ~128);
            flags = add_admins ? (flags | 512) : (flags & ~512);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            change_info = (flags & 1) != 0;
            post_messages = (flags & 2) != 0;
            edit_messages = (flags & 4) != 0;
            delete_messages = (flags & 8) != 0;
            ban_users = (flags & 16) != 0;
            invite_users = (flags & 32) != 0;
            invite_link = (flags & 64) != 0;
            pin_messages = (flags & 128) != 0;
            add_admins = (flags & 512) != 0;

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);










        }
    }
}
