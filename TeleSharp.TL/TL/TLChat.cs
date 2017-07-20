using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-652419756)]
    public class TLChat : TLAbsChat
    {
        public override int Constructor
        {
            get
            {
                return -652419756;
            }
        }

        public int flags { get; set; }
        public bool creator { get; set; }
        public bool kicked { get; set; }
        public bool left { get; set; }
        public bool admins_enabled { get; set; }
        public bool admin { get; set; }
        public bool deactivated { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public TLAbsChatPhoto photo { get; set; }
        public int participants_count { get; set; }
        public int date { get; set; }
        public int version { get; set; }
        public TLAbsInputChannel migrated_to { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = creator ? (flags | 1) : (flags & ~1);
            flags = kicked ? (flags | 2) : (flags & ~2);
            flags = left ? (flags | 4) : (flags & ~4);
            flags = admins_enabled ? (flags | 8) : (flags & ~8);
            flags = admin ? (flags | 16) : (flags & ~16);
            flags = deactivated ? (flags | 32) : (flags & ~32);
            flags = migrated_to != null ? (flags | 64) : (flags & ~64);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            creator = (flags & 1) != 0;
            kicked = (flags & 2) != 0;
            left = (flags & 4) != 0;
            admins_enabled = (flags & 8) != 0;
            admin = (flags & 16) != 0;
            deactivated = (flags & 32) != 0;
            id = br.ReadInt32();
            title = StringUtil.Deserialize(br);
            photo = (TLAbsChatPhoto)ObjectUtils.DeserializeObject(br);
            participants_count = br.ReadInt32();
            date = br.ReadInt32();
            version = br.ReadInt32();
            if ((flags & 64) != 0)
                migrated_to = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            else
                migrated_to = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);






            bw.Write(id);
            StringUtil.Serialize(title, bw);
            ObjectUtils.SerializeObject(photo, bw);
            bw.Write(participants_count);
            bw.Write(date);
            bw.Write(version);
            if ((flags & 64) != 0)
                ObjectUtils.SerializeObject(migrated_to, bw);

        }
    }
}
