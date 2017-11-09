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

        public int Flags { get; set; }
        public bool Creator { get; set; }
        public bool Kicked { get; set; }
        public bool Left { get; set; }
        public bool Editor { get; set; }
        public bool Moderator { get; set; }
        public bool Broadcast { get; set; }
        public bool Verified { get; set; }
        public bool Megagroup { get; set; }
        public bool Restricted { get; set; }
        public bool Democracy { get; set; }
        public bool Signatures { get; set; }
        public bool Min { get; set; }
        public int Id { get; set; }
        public long? AccessHash { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public TLAbsChatPhoto Photo { get; set; }
        public int Date { get; set; }
        public int Version { get; set; }
        public string RestrictionReason { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Creator ? (Flags | 1) : (Flags & ~1);
            Flags = Kicked ? (Flags | 2) : (Flags & ~2);
            Flags = Left ? (Flags | 4) : (Flags & ~4);
            Flags = Editor ? (Flags | 8) : (Flags & ~8);
            Flags = Moderator ? (Flags | 16) : (Flags & ~16);
            Flags = Broadcast ? (Flags | 32) : (Flags & ~32);
            Flags = Verified ? (Flags | 128) : (Flags & ~128);
            Flags = Megagroup ? (Flags | 256) : (Flags & ~256);
            Flags = Restricted ? (Flags | 512) : (Flags & ~512);
            Flags = Democracy ? (Flags | 1024) : (Flags & ~1024);
            Flags = Signatures ? (Flags | 2048) : (Flags & ~2048);
            Flags = Min ? (Flags | 4096) : (Flags & ~4096);
            Flags = AccessHash != null ? (Flags | 8192) : (Flags & ~8192);
            Flags = Username != null ? (Flags | 64) : (Flags & ~64);
            Flags = RestrictionReason != null ? (Flags | 512) : (Flags & ~512);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Creator = (Flags & 1) != 0;
            Kicked = (Flags & 2) != 0;
            Left = (Flags & 4) != 0;
            Editor = (Flags & 8) != 0;
            Moderator = (Flags & 16) != 0;
            Broadcast = (Flags & 32) != 0;
            Verified = (Flags & 128) != 0;
            Megagroup = (Flags & 256) != 0;
            Restricted = (Flags & 512) != 0;
            Democracy = (Flags & 1024) != 0;
            Signatures = (Flags & 2048) != 0;
            Min = (Flags & 4096) != 0;
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
                RestrictionReason = StringUtil.Deserialize(br);
            else
                RestrictionReason = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
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
                StringUtil.Serialize(RestrictionReason, bw);

        }
    }
}
