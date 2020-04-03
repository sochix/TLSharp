using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1605510357)]
    public class TLChatAdminRights : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1605510357;
            }
        }

        public int Flags { get; set; }
        public bool ChangeInfo { get; set; }
        public bool PostMessages { get; set; }
        public bool EditMessages { get; set; }
        public bool DeleteMessages { get; set; }
        public bool BanUsers { get; set; }
        public bool InviteUsers { get; set; }
        public bool PinMessages { get; set; }
        public bool AddAdmins { get; set; }


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
