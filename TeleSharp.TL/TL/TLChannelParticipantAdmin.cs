using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1473271656)]
    public class TLChannelParticipantAdmin : TLAbsChannelParticipant
    {
        public TLChannelAdminRights AdminRights { get; set; }

        public bool CanEdit { get; set; }

        public override int Constructor
        {
            get
            {
                return -1473271656;
            }
        }

        public int Date { get; set; }

        public int Flags { get; set; }

        public int InviterId { get; set; }

        public int PromotedBy { get; set; }

        public int UserId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            CanEdit = (Flags & 1) != 0;
            UserId = br.ReadInt32();
            InviterId = br.ReadInt32();
            PromotedBy = br.ReadInt32();
            Date = br.ReadInt32();
            AdminRights = (TLChannelAdminRights)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            bw.Write(UserId);
            bw.Write(InviterId);
            bw.Write(PromotedBy);
            bw.Write(Date);
            ObjectUtils.SerializeObject(AdminRights, bw);
        }
    }
}
