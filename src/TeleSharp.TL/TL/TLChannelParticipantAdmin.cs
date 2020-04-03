using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-859915345)]
    public class TLChannelParticipantAdmin : TLAbsChannelParticipant
    {
        public override int Constructor
        {
            get
            {
                return -859915345;
            }
        }

        public int Flags { get; set; }
        public bool CanEdit { get; set; }
        public bool Self { get; set; }
        public int UserId { get; set; }
        public int? InviterId { get; set; }
        public int PromotedBy { get; set; }
        public int Date { get; set; }
        public TLChatAdminRights AdminRights { get; set; }
        public string Rank { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            CanEdit = (Flags & 1) != 0;
            Self = (Flags & 2) != 0;
            UserId = br.ReadInt32();
            if ((Flags & 2) != 0)
                InviterId = br.ReadInt32();
            else
                InviterId = null;

            PromotedBy = br.ReadInt32();
            Date = br.ReadInt32();
            AdminRights = (TLChatAdminRights)ObjectUtils.DeserializeObject(br);
            if ((Flags & 4) != 0)
                Rank = StringUtil.Deserialize(br);
            else
                Rank = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);


            bw.Write(UserId);
            if ((Flags & 2) != 0)
                bw.Write(InviterId.Value);
            bw.Write(PromotedBy);
            bw.Write(Date);
            ObjectUtils.SerializeObject(AdminRights, bw);
            if ((Flags & 4) != 0)
                StringUtil.Serialize(Rank, bw);

        }
    }
}
