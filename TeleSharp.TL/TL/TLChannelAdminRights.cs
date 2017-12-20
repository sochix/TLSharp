using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1568467877)]
    public class TLChannelAdminRights : TLObject
    {
        public bool AddAdmins { get; set; }

        public bool BanUsers { get; set; }

        public bool ChangeInfo { get; set; }

        public override int Constructor
        {
            get
            {
                return 1568467877;
            }
        }

        public bool DeleteMessages { get; set; }

        public bool EditMessages { get; set; }

        public int Flags { get; set; }

        public bool InviteLink { get; set; }

        public bool InviteUsers { get; set; }

        public bool PinMessages { get; set; }

        public bool PostMessages { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            ChangeInfo = (Flags & 1) != 0;
            PostMessages = (Flags & 2) != 0;
            EditMessages = (Flags & 4) != 0;
            DeleteMessages = (Flags & 8) != 0;
            BanUsers = (Flags & 16) != 0;
            InviteUsers = (Flags & 32) != 0;
            InviteLink = (Flags & 64) != 0;
            PinMessages = (Flags & 128) != 0;
            AddAdmins = (Flags & 512) != 0;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
        }
    }
}
