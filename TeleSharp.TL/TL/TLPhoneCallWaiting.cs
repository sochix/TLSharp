using System.IO;

namespace TeleSharp.TL
{
    [TLObject(462375633)]
    public class TLPhoneCallWaiting : TLAbsPhoneCall
    {
        public long AccessHash { get; set; }

        public int AdminId { get; set; }

        public override int Constructor
        {
            get
            {
                return 462375633;
            }
        }

        public int Date { get; set; }

        public int Flags { get; set; }

        public long Id { get; set; }

        public int ParticipantId { get; set; }

        public TLPhoneCallProtocol Protocol { get; set; }

        public int? ReceiveDate { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Id = br.ReadInt64();
            AccessHash = br.ReadInt64();
            Date = br.ReadInt32();
            AdminId = br.ReadInt32();
            ParticipantId = br.ReadInt32();
            Protocol = (TLPhoneCallProtocol)ObjectUtils.DeserializeObject(br);
            if ((Flags & 1) != 0)
                ReceiveDate = br.ReadInt32();
            else
                ReceiveDate = null;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            bw.Write(Id);
            bw.Write(AccessHash);
            bw.Write(Date);
            bw.Write(AdminId);
            bw.Write(ParticipantId);
            ObjectUtils.SerializeObject(Protocol, bw);
            if ((Flags & 1) != 0)
                bw.Write(ReceiveDate.Value);
        }
    }
}
