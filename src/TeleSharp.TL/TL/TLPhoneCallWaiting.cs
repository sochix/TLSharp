using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(462375633)]
    public class TLPhoneCallWaiting : TLAbsPhoneCall
    {
        public override int Constructor
        {
            get
            {
                return 462375633;
            }
        }

        public int Flags { get; set; }
        public long Id { get; set; }
        public long AccessHash { get; set; }
        public int Date { get; set; }
        public int AdminId { get; set; }
        public int ParticipantId { get; set; }
        public TLPhoneCallProtocol Protocol { get; set; }
        public int? ReceiveDate { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = ReceiveDate != null ? (Flags | 1) : (Flags & ~1);

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
            ComputeFlags();
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
