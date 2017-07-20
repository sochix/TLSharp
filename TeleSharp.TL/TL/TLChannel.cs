using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1588737454)]
    public class TLChannel : TLAbsChat
    {
        public override int Constructor
        {
            get
            {
                return -1588737454;
            }
        }

        public int flags { get; set; }
        public bool creator { get; set; }
        public bool kicked { get; set; }
        public bool left { get; set; }
        public bool editor { get; set; }
        public bool moderator { get; set; }
        public bool broadcast { get; set; }
        public bool verified { get; set; }
        public bool megagroup { get; set; }
        public bool restricted { get; set; }
        public bool democracy { get; set; }
        public bool signatures { get; set; }
        public bool min { get; set; }
        public int id { get; set; }
        public long? access_hash { get; set; }
        public string title { get; set; }
        public string username { get; set; }
        public TLAbsChatPhoto photo { get; set; }
        public int date { get; set; }
        public int version { get; set; }
        public string restriction_reason { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = creator ? (flags | 1) : (flags & ~1);
            flags = kicked ? (flags | 2) : (flags & ~2);
            flags = left ? (flags | 4) : (flags & ~4);
            flags = editor ? (flags | 8) : (flags & ~8);
            flags = moderator ? (flags | 16) : (flags & ~16);
            flags = broadcast ? (flags | 32) : (flags & ~32);
            flags = verified ? (flags | 128) : (flags & ~128);
            flags = megagroup ? (flags | 256) : (flags & ~256);
            flags = restricted ? (flags | 512) : (flags & ~512);
            flags = democracy ? (flags | 1024) : (flags & ~1024);
            flags = signatures ? (flags | 2048) : (flags & ~2048);
            flags = min ? (flags | 4096) : (flags & ~4096);
            flags = access_hash != null ? (flags | 8192) : (flags & ~8192);
            flags = username != null ? (flags | 64) : (flags & ~64);
            flags = restriction_reason != null ? (flags | 512) : (flags & ~512);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            creator = (flags & 1) != 0;
            kicked = (flags & 2) != 0;
            left = (flags & 4) != 0;
            editor = (flags & 8) != 0;
            moderator = (flags & 16) != 0;
            broadcast = (flags & 32) != 0;
            verified = (flags & 128) != 0;
            megagroup = (flags & 256) != 0;
            restricted = (flags & 512) != 0;
            democracy = (flags & 1024) != 0;
            signatures = (flags & 2048) != 0;
            min = (flags & 4096) != 0;
            id = br.ReadInt32();
            if ((flags & 8192) != 0)
                access_hash = br.ReadInt64();
            else
                access_hash = null;

            title = StringUtil.Deserialize(br);
            if ((flags & 64) != 0)
                username = StringUtil.Deserialize(br);
            else
                username = null;

            photo = (TLAbsChatPhoto)ObjectUtils.DeserializeObject(br);
            date = br.ReadInt32();
            version = br.ReadInt32();
            if ((flags & 512) != 0)
                restriction_reason = StringUtil.Deserialize(br);
            else
                restriction_reason = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);












            bw.Write(id);
            if ((flags & 8192) != 0)
                bw.Write(access_hash.Value);
            StringUtil.Serialize(title, bw);
            if ((flags & 64) != 0)
                StringUtil.Serialize(username, bw);
            ObjectUtils.SerializeObject(photo, bw);
            bw.Write(date);
            bw.Write(version);
            if ((flags & 512) != 0)
                StringUtil.Serialize(restriction_reason, bw);

        }
    }
}
