using System.IO;
namespace TeleSharp.TL
{
    [TLObject(573315206)]
    public class TLChannelParticipantBanned : TLAbsChannelParticipant
    {
        public override int Constructor
        {
            get
            {
                return 573315206;
            }
        }

        public int flags { get; set; }
        public bool left { get; set; }
        public int user_id { get; set; }
        public int kicked_by { get; set; }
        public int date { get; set; }
        public TLChannelBannedRights banned_rights { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = left ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            left = (flags & 1) != 0;
            user_id = br.ReadInt32();
            kicked_by = br.ReadInt32();
            date = br.ReadInt32();
            banned_rights = (TLChannelBannedRights)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            bw.Write(user_id);
            bw.Write(kicked_by);
            bw.Write(date);
            ObjectUtils.SerializeObject(banned_rights, bw);

        }
    }
}
