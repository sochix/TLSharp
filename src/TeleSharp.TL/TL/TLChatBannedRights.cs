using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1626209256)]
    public class TLChatBannedRights : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1626209256;
            }
        }

        public int Flags { get; set; }
        public bool ViewMessages { get; set; }
        public bool SendMessages { get; set; }
        public bool SendMedia { get; set; }
        public bool SendStickers { get; set; }
        public bool SendGifs { get; set; }
        public bool SendGames { get; set; }
        public bool SendInline { get; set; }
        public bool EmbedLinks { get; set; }
        public bool SendPolls { get; set; }
        public bool ChangeInfo { get; set; }
        public bool InviteUsers { get; set; }
        public bool PinMessages { get; set; }
        public int UntilDate { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            ViewMessages = (Flags & 1) != 0;
            SendMessages = (Flags & 2) != 0;
            SendMedia = (Flags & 4) != 0;
            SendStickers = (Flags & 8) != 0;
            SendGifs = (Flags & 16) != 0;
            SendGames = (Flags & 32) != 0;
            SendInline = (Flags & 64) != 0;
            EmbedLinks = (Flags & 128) != 0;
            SendPolls = (Flags & 256) != 0;
            ChangeInfo = (Flags & 1024) != 0;
            InviteUsers = (Flags & 32768) != 0;
            PinMessages = (Flags & 131072) != 0;
            UntilDate = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);












            bw.Write(UntilDate);

        }
    }
}
