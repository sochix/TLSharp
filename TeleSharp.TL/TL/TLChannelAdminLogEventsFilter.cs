using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-368018716)]
    public class TLChannelAdminLogEventsFilter : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -368018716;
            }
        }

        public int flags { get; set; }
        public bool @join { get; set; }
        public bool leave { get; set; }
        public bool invite { get; set; }
        public bool ban { get; set; }
        public bool unban { get; set; }
        public bool kick { get; set; }
        public bool unkick { get; set; }
        public bool promote { get; set; }
        public bool demote { get; set; }
        public bool info { get; set; }
        public bool settings { get; set; }
        public bool pinned { get; set; }
        public bool edit { get; set; }
        public bool delete { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = @join ? (flags | 1) : (flags & ~1);
            flags = leave ? (flags | 2) : (flags & ~2);
            flags = invite ? (flags | 4) : (flags & ~4);
            flags = ban ? (flags | 8) : (flags & ~8);
            flags = unban ? (flags | 16) : (flags & ~16);
            flags = kick ? (flags | 32) : (flags & ~32);
            flags = unkick ? (flags | 64) : (flags & ~64);
            flags = promote ? (flags | 128) : (flags & ~128);
            flags = demote ? (flags | 256) : (flags & ~256);
            flags = info ? (flags | 512) : (flags & ~512);
            flags = settings ? (flags | 1024) : (flags & ~1024);
            flags = pinned ? (flags | 2048) : (flags & ~2048);
            flags = edit ? (flags | 4096) : (flags & ~4096);
            flags = delete ? (flags | 8192) : (flags & ~8192);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            @join = (flags & 1) != 0;
            leave = (flags & 2) != 0;
            invite = (flags & 4) != 0;
            ban = (flags & 8) != 0;
            unban = (flags & 16) != 0;
            kick = (flags & 32) != 0;
            unkick = (flags & 64) != 0;
            promote = (flags & 128) != 0;
            demote = (flags & 256) != 0;
            info = (flags & 512) != 0;
            settings = (flags & 1024) != 0;
            pinned = (flags & 2048) != 0;
            edit = (flags & 4096) != 0;
            delete = (flags & 8192) != 0;

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);















        }
    }
}
