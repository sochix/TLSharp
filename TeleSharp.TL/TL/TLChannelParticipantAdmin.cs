using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-1473271656)]
    public class TLChannelParticipantAdmin : TLAbsChannelParticipant
    {
        public override int Constructor
        {
            get
            {
                return -1473271656;
            }
        }

        public int flags { get; set; }
        public bool can_edit { get; set; }
        public int user_id { get; set; }
        public int inviter_id { get; set; }
        public int promoted_by { get; set; }
        public int date { get; set; }
        public TLChannelAdminRights admin_rights { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = can_edit ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            can_edit = (flags & 1) != 0;
            user_id = br.ReadInt32();
            inviter_id = br.ReadInt32();
            promoted_by = br.ReadInt32();
            date = br.ReadInt32();
            admin_rights = (TLChannelAdminRights)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            bw.Write(user_id);
            bw.Write(inviter_id);
            bw.Write(promoted_by);
            bw.Write(date);
            ObjectUtils.SerializeObject(admin_rights, bw);

        }
    }
}
