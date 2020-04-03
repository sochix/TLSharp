using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
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

        public int Flags { get; set; }
        public bool Join { get; set; }
        public bool Leave { get; set; }
        public bool Invite { get; set; }
        public bool Ban { get; set; }
        public bool Unban { get; set; }
        public bool Kick { get; set; }
        public bool Unkick { get; set; }
        public bool Promote { get; set; }
        public bool Demote { get; set; }
        public bool Info { get; set; }
        public bool Settings { get; set; }
        public bool Pinned { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Join = (Flags & 1) != 0;
            Leave = (Flags & 2) != 0;
            Invite = (Flags & 4) != 0;
            Ban = (Flags & 8) != 0;
            Unban = (Flags & 16) != 0;
            Kick = (Flags & 32) != 0;
            Unkick = (Flags & 64) != 0;
            Promote = (Flags & 128) != 0;
            Demote = (Flags & 256) != 0;
            Info = (Flags & 512) != 0;
            Settings = (Flags & 1024) != 0;
            Pinned = (Flags & 2048) != 0;
            Edit = (Flags & 4096) != 0;
            Delete = (Flags & 8192) != 0;

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);















        }
    }
}
